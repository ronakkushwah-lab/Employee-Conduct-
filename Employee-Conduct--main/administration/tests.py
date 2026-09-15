from datetime import date
from unittest.mock import patch

from django.contrib.auth.hashers import check_password
from django.core.files.uploadedfile import SimpleUploadedFile
from django.test import TestCase
from django.urls import reverse

from account.models import Company, CompanyStaff
from administration.models import notification
from employee.models import Department, Employee, Post
from managers.models import Manager


class AdminEndToEndTests(TestCase):
    def setUp(self):
        self.company = Company.objects.create(company_name='Acme')
        self.other_company = Company.objects.create(company_name='Other')
        self.admin = CompanyStaff.objects.create(
            company=self.company,
            email='admin@acme.test',
            password='admin-pass',
            role=CompanyStaff.ROLE_ADMIN,
            is_company_admin=True,
            is_authenticated=True,
        )
        self.department = Department.objects.create(
            company=self.company, department_name='Engineering'
        )
        manager_staff = CompanyStaff.objects.create(
            company=self.company,
            email='manager@acme.test',
            password='manager-pass',
            is_manager=True,
            is_authenticated=True,
        )
        self.manager = Manager.objects.create(
            user=manager_staff,
            manager_first_name='Mina',
            manager_last_name='Ger',
            manager_email=manager_staff.email,
            manager_joining_date=date(2025, 1, 1),
            manager_department=self.department,
            manager_designation='Lead',
            manager_id='EIC-M01',
        )
        employee_staff = CompanyStaff.objects.create(
            company=self.company,
            email='employee@acme.test',
            password='employee-pass',
            is_employee=True,
            is_authenticated=True,
        )
        self.employee = Employee.objects.create(
            user=employee_staff,
            employee_first_name='Em',
            employee_last_name='Ployee',
            employee_email=employee_staff.email,
            employee_joining_date=date(2025, 1, 2),
            employee_department=self.department,
            employee_designation='Developer',
            employee_id='EIC-E01',
            employee_reports_to=self.manager,
        )
        session = self.client.session
        session['company_staff_id'] = self.admin.id
        session.save()

    def url(self, name, **extra):
        values = {
            'company_id': self.company.id,
            'company_staff_id': self.admin.id,
        }
        values.update(extra)
        return reverse(name, kwargs=values)

    def test_critical_admin_pages_render(self):
        urls = [
            self.url('registermanager'),
            self.url('resignrejected'),
            self.url('alldocument'),
            self.url('invoices'),
        ]
        for url in urls:
            with self.subTest(url=url):
                self.assertEqual(self.client.get(url).status_code, 200)

    def test_company_json_endpoints_do_not_leak_other_company_data(self):
        notification.objects.create(company=self.company, notify='visible')
        notification.objects.create(company=self.other_company, notify='hidden')
        response = self.client.get(reverse('getnotification', args=[self.company.id]))
        self.assertEqual(response.status_code, 200)
        self.assertEqual(response.json(), {'notify': [{'notify': 'visible'}]})

    def test_manager_delete_uses_post_and_preserves_reporting_employee(self):
        url = self.url('Remove_manager', id=self.manager.id)
        self.assertEqual(self.client.get(url).status_code, 405)
        response = self.client.post(url)
        self.assertEqual(response.status_code, 302)
        self.employee.refresh_from_db()
        self.assertIsNone(self.employee.employee_reports_to)
        self.assertFalse(Manager.objects.filter(id=self.manager.id).exists())

    @patch('administration.email_notifications.send_new_user_notification')
    def test_manager_registration_stores_a_working_single_hash(self, _send_email):
        response = self.client.post(self.url('registermanager'), {
            'manager_first_name': 'New',
            'manager_last_name': 'Manager',
            'manager_email': 'new-manager@acme.test',
            'manager_joining_date': '2026-01-15',
            'manager_password': 'Strong-pass-123',
            'manager_confirm_password': 'Strong-pass-123',
            'manager_id': 'EIC-M02',
            'manager_phone': '1234567890',
            'manager_salary': '50000',
            'id': self.department.id,
        })
        self.assertEqual(response.status_code, 302)
        staff = CompanyStaff.objects.get(email='new-manager@acme.test')
        self.assertTrue(check_password('Strong-pass-123', staff.password))
        self.assertTrue(Manager.objects.filter(user=staff).exists())

    def test_document_upload_rejects_unsupported_files(self):
        upload = SimpleUploadedFile(
            'payload.exe', b'not executable', content_type='application/octet-stream'
        )
        response = self.client.post(self.url('all_documents'), {
            'employee': self.employee.id,
            'experience_letter': upload,
        })
        self.assertEqual(response.status_code, 302)
        self.assertFalse(Post.objects.filter(user=self.employee).exists())
