from datetime import date, timedelta
from unittest.mock import patch

from django.core.files.uploadedfile import SimpleUploadedFile
from django.test import TestCase
from django.urls import reverse
from django.utils import timezone

from account.models import Company, CompanyStaff
from employee.models import Attendance, Department, Employee, Entries, Post
from leave.models import BalanceLeaves, Leave
from managers.models import Manager
from regularization.models import Regularization
from resign.models import Resign


class EmployeeRoleEndToEndTests(TestCase):
    def setUp(self):
        self.company = Company.objects.create(company_name='Employee Co')
        self.other_company = Company.objects.create(company_name='Other Co')
        self.department = Department.objects.create(
            company=self.company, department_name='Engineering'
        )
        manager_staff = CompanyStaff.objects.create(
            company=self.company, email='manager@employee.test', password='pass',
            is_manager=True, is_authenticated=True,
        )
        self.manager = Manager.objects.create(
            user=manager_staff,
            manager_first_name='Test', manager_last_name='Manager',
            manager_email=manager_staff.email,
            manager_joining_date=date(2025, 1, 1),
            manager_department=self.department,
            manager_designation='Lead', manager_id='M-1',
        )
        self.staff = CompanyStaff.objects.create(
            company=self.company, email='employee@employee.test', password='pass',
            is_employee=True, is_authenticated=True,
        )
        self.employee = Employee.objects.create(
            user=self.staff,
            employee_first_name='Test', employee_last_name='Employee',
            employee_email=self.staff.email,
            employee_joining_date=date(2025, 1, 2),
            employee_department=self.department,
            employee_designation='Developer', employee_id='E-1',
            employee_reports_to=self.manager,
        )
        BalanceLeaves.objects.create(user=self.employee, balancedays=20)
        session = self.client.session
        session['company_staff_id'] = self.staff.id
        session.save()

    def url(self, name, **extra):
        values = {
            'company_id': self.company.id,
            'company_staff_id': self.staff.id,
        }
        values.update(extra)
        return reverse(name, kwargs=values)

    def test_employee_pages_render(self):
        urls = [
            self.url('employee_dashboard'), self.url('employee_profile'),
            self.url('attendance'), self.url('task-list'),
            self.url('salary-list'), self.url('notification'),
            self.url('create_entry'), self.url('entries-detail'),
            reverse('entries-create'),
            self.url('create_leave'), self.url('leave'), self.url('staffleavetable'),
            self.url('leavebalance'), self.url('holiday_list'),
            self.url('create_resign'), self.url('resign'), self.url('staffresigntable'),
            self.url('regularization_required'), self.url('create_regularization'),
            self.url('regularization'),
            self.url('all_document_Views'), self.url('mynotifications'),
        ]
        for url in urls:
            with self.subTest(url=url):
                self.assertEqual(self.client.get(url).status_code, 200)

    def test_company_id_cannot_be_swapped_to_read_employee_data(self):
        response = self.client.get(reverse('employee_profile', kwargs={
            'company_id': self.other_company.id,
            'company_staff_id': self.staff.id,
        }))
        self.assertEqual(response.status_code, 302)

    def test_profile_update_allows_personal_fields_but_not_privileged_fields(self):
        response = self.client.post(self.url('employee_profile'), {
            'employee_phone': '9876543210',
            'employee_status': 'Inactive',
            'employee_salary': '99999999',
            'employee_email': 'changed@example.test',
        })
        self.assertEqual(response.status_code, 302)
        self.employee.refresh_from_db()
        self.assertEqual(self.employee.employee_phone, '+91 98765 43210')
        self.assertEqual(self.employee.employee_status, 'Active')
        self.assertNotEqual(self.employee.employee_salary, '99999999')
        self.assertEqual(self.employee.employee_email, self.staff.email)

    @patch('administration.email_notifications.send_attendance_notification')
    def test_employee_can_check_in_and_out(self, _send_notification):
        headers = {'HTTP_X_REQUESTED_WITH': 'XMLHttpRequest'}
        check_in = self.client.post(
            self.url('attendance_post'), {'is_check_in': 'true'}, **headers
        )
        self.assertEqual(check_in.status_code, 200)
        attendance = Attendance.objects.get(employee=self.employee)
        check_out = self.client.post(self.url('attendance_post'), {
            'is_check_in': 'false', 'attendance_id': attendance.id,
        }, **headers)
        self.assertEqual(check_out.status_code, 200)
        attendance.refresh_from_db()
        self.assertIsNotNone(attendance.check_out)

    def test_attendance_rejects_get_and_checkout_before_checkin(self):
        self.assertEqual(self.client.get(self.url('attendance_post')).status_code, 405)
        response = self.client.post(
            self.url('attendance_post'), {'is_check_in': 'false'},
            HTTP_X_REQUESTED_WITH='XMLHttpRequest',
        )
        self.assertEqual(response.status_code, 400)
        self.assertFalse(Attendance.objects.exists())

    @patch('administration.email_notifications.send_leave_submission_notification')
    @patch('administration.email_notifications.send_simple_email_to_manager')
    def test_leave_creation_is_owned_by_employee(self, _manager_email, _submission_email):
        start = date.today() + timedelta(days=2)
        response = self.client.post(self.url('create_leave'), {
            'startdate': start.isoformat(),
            'enddate': (start + timedelta(days=1)).isoformat(),
            'leavetype': 'casual', 'reason': 'Family event',
            'manager_id': self.manager.id,
        })
        self.assertEqual(response.status_code, 302)
        leave = Leave.objects.get()
        self.assertEqual(leave.user, self.employee)
        self.assertEqual(leave.manager, self.manager)

    def test_overlapping_leave_is_rejected(self):
        start = date.today() + timedelta(days=4)
        Leave.objects.create(
            user=self.employee, manager=self.manager,
            startdate=start, enddate=start + timedelta(days=2),
            leavetype='casual', reason='Existing',
        )
        response = self.client.post(self.url('create_leave'), {
            'startdate': (start + timedelta(days=1)).isoformat(),
            'enddate': (start + timedelta(days=3)).isoformat(),
            'leavetype': 'sick', 'reason': 'Overlap',
            'manager_id': self.manager.id,
        })
        self.assertEqual(response.status_code, 200)
        self.assertEqual(Leave.objects.count(), 1)

    def test_invalid_regularization_range_is_rejected(self):
        now = timezone.localtime().replace(microsecond=0)
        response = self.client.post(self.url('create_regularization'), {
            'check_in': now.isoformat(timespec='minutes'),
            'check_out': (now - timedelta(hours=1)).isoformat(timespec='minutes'),
            'reason': 'Incorrect punch', 'manager_id': self.manager.id,
        })
        self.assertEqual(response.status_code, 200)
        self.assertFalse(Regularization.objects.exists())

    def test_unsupported_document_is_rejected(self):
        upload = SimpleUploadedFile(
            'payload.exe', b'not executable', content_type='application/octet-stream'
        )
        response = self.client.post(self.url('create_ducuments'), {
            'experience_letter': upload,
        })
        self.assertEqual(response.status_code, 200)
        self.assertFalse(Post.objects.exists())

    def test_invalid_profile_image_is_rejected(self):
        upload = SimpleUploadedFile(
            'avatar.png', b'not an image', content_type='image/png'
        )
        response = self.client.post(self.url('upload_profile_image'), {
            'employee_image': upload,
        })
        self.assertEqual(response.status_code, 400)
        self.employee.refresh_from_db()
        self.assertFalse(self.employee.employee_image)

    def test_timesheet_delete_is_post_only_and_owner_scoped(self):
        start = timezone.now() - timedelta(hours=2)
        entry = Entries.objects.create(
            user=self.employee, start_time=start, end_time=start + timedelta(hours=1),
            project='Project', task='Task', assigned_to=self.manager,
        )
        url = self.url('entry_remove', id=entry.id)
        self.assertEqual(self.client.get(url).status_code, 405)
        self.assertEqual(self.client.post(url).status_code, 302)
        self.assertFalse(Entries.objects.filter(id=entry.id).exists())
