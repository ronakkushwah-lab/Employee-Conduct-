import datetime
from django.test import TestCase, Client
from django.utils import timezone
from django.contrib.auth.hashers import make_password

from account.models import Company, CompanyStaff, User
from employee.models import Employee, Department, Designation, Attendance
from managers.models import Manager


class ManagerAttendanceRegisterTests(TestCase):
    def setUp(self):
        self.client = Client()
        self.company = Company.objects.create(
            company_name='Eagle In Cloud Test',
            name='Eagle In Cloud Test',
            company_email='info@eagleinclouds.com'
        )

        # Create 2 Departments
        self.dept_engineering = Department.objects.create(
            company=self.company,
            department_name='Data Engineering'
        )
        self.dept_sales = Department.objects.create(
            company=self.company,
            department_name='Sales & Marketing'
        )

        # Create Manager Staff & Manager Record
        self.manager_staff = CompanyStaff.objects.create(
            company=self.company,
            email='aditya.manager@eagleincloud.test',
            password=make_password('SecurePass123!'),
            role=CompanyStaff.ROLE_MANAGER,
            is_manager=True,
            is_authenticated=True,
            password_changed_at=timezone.now()
        )
        self.manager = Manager.objects.create(
            user=self.manager_staff,
            manager_first_name='Aditya',
            manager_last_name='Sharma',
            manager_email=self.manager_staff.email,
            manager_id='MGR-001',
            manager_joining_date=timezone.localdate(),
            manager_designation='Engineering Lead',
            manager_department=self.dept_engineering
        )

        # Create 2 Employees in Data Engineering
        self.emp_eng1_staff = CompanyStaff.objects.create(
            company=self.company,
            email='divyansh@eagleincloud.test',
            password=make_password('SecurePass123!'),
            role=CompanyStaff.ROLE_EMPLOYEE,
            is_employee=True,
            is_authenticated=True,
            password_changed_at=timezone.now()
        )
        self.emp_eng1 = Employee.objects.create(
            user=self.emp_eng1_staff,
            employee_first_name='Divyansh',
            employee_last_name='Mishra',
            employee_id='EIC/DE/001',
            employee_joining_date=timezone.localdate(),
            employee_designation='Data Engineer',
            employee_email=self.emp_eng1_staff.email,
            employee_department=self.dept_engineering,
            employee_status='Active'
        )

        self.emp_eng2_staff = CompanyStaff.objects.create(
            company=self.company,
            email='gaurav@eagleincloud.test',
            password=make_password('SecurePass123!'),
            role=CompanyStaff.ROLE_EMPLOYEE,
            is_employee=True,
            is_authenticated=True,
            password_changed_at=timezone.now()
        )
        self.emp_eng2 = Employee.objects.create(
            user=self.emp_eng2_staff,
            employee_first_name='Gaurav',
            employee_last_name='Ubhad',
            employee_id='EIC/DE/002',
            employee_joining_date=timezone.localdate(),
            employee_designation='Data Engineer',
            employee_email=self.emp_eng2_staff.email,
            employee_department=self.dept_engineering,
            employee_status='Active'
        )

        # Create 1 Employee in Sales (Should NEVER be visible to engineering manager)
        self.emp_sales_staff = CompanyStaff.objects.create(
            company=self.company,
            email='rohit.sales@eagleincloud.test',
            password=make_password('SecurePass123!'),
            role=CompanyStaff.ROLE_EMPLOYEE,
            is_employee=True,
            is_authenticated=True,
            password_changed_at=timezone.now()
        )
        self.emp_sales = Employee.objects.create(
            user=self.emp_sales_staff,
            employee_first_name='Rohit',
            employee_last_name='Verma',
            employee_id='EIC/SL/001',
            employee_joining_date=timezone.localdate(),
            employee_designation='Sales Executive',
            employee_email=self.emp_sales_staff.email,
            employee_department=self.dept_sales,
            employee_status='Active'
        )

        # Create Attendance record for today
        today = timezone.localdate()
        dt_in = timezone.make_aware(datetime.datetime.combine(today, datetime.time(9, 30)))
        dt_out = timezone.make_aware(datetime.datetime.combine(today, datetime.time(18, 30)))
        self.att_eng1 = Attendance.objects.create(
            employee=self.emp_eng1,
            check_in=dt_in,
            check_out=dt_out
        )
        self.att_sales = Attendance.objects.create(
            employee=self.emp_sales,
            check_in=dt_in,
            check_out=dt_out
        )

    def test_manager_attendance_register_department_scoping(self):
        """Verify manager sees only their department employees and NOT other departments."""
        url = f'/hrms/managers/attendance_register/{self.company.id}/{self.manager_staff.id}/'
        response = self.client.get(url)
        self.assertEqual(response.status_code, 200)

        matrix_rows = response.context['matrix_rows']
        emp_names = [row['meta']['name'] for row in matrix_rows]

        # Engineering employees must be present
        self.assertIn('Divyansh Mishra', emp_names)
        self.assertIn('Gaurav Ubhad', emp_names)
        # Sales employee must NEVER be present
        self.assertNotIn('Rohit Verma', emp_names)
        self.assertEqual(len(matrix_rows), 2)

    def test_manager_daily_roster_scoping(self):
        """Verify Daily mode for manager contains only department employees."""
        today_str = timezone.localdate().strftime('%Y-%m-%d')
        url = f'/hrms/managers/attendance_register/{self.company.id}/{self.manager_staff.id}/?period=daily&date={today_str}'
        response = self.client.get(url)
        self.assertEqual(response.status_code, 200)

        daily_rows = response.context['daily_rows']
        emp_names = [row['meta']['name'] for row in daily_rows]
        self.assertIn('Divyansh Mishra', emp_names)
        self.assertNotIn('Rohit Verma', emp_names)

    def test_manager_yearly_summary_scoping(self):
        """Verify Yearly mode for manager contains only department employees."""
        year = timezone.localdate().year
        url = f'/hrms/managers/attendance_register/{self.company.id}/{self.manager_staff.id}/?period=yearly&year={year}'
        response = self.client.get(url)
        self.assertEqual(response.status_code, 200)

        yearly_rows = response.context['yearly_rows']
        emp_names = [row['meta']['name'] for row in yearly_rows]
        self.assertIn('Divyansh Mishra', emp_names)
        self.assertNotIn('Rohit Verma', emp_names)

    def test_manager_export_excel_scoping(self):
        """Verify Excel export endpoint scopes to department with 200 response."""
        url = f'/hrms/managers/attendance_register/export/{self.company.id}/{self.manager_staff.id}/?period=monthly&format=excel'
        response = self.client.get(url)
        self.assertEqual(response.status_code, 200)
        self.assertIn(response['Content-Type'], [
            'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
            'application/octet-stream',
            'text/csv'
        ])

    def test_manager_export_csv_scoping(self):
        """Verify CSV export endpoint scopes to department and returns CSV content."""
        url = f'/hrms/managers/attendance_register/export/{self.company.id}/{self.manager_staff.id}/?period=monthly&format=csv'
        response = self.client.get(url)
        self.assertEqual(response.status_code, 200)
        self.assertIn('text/csv', response['Content-Type'])
        content = response.content.decode('utf-8')
        self.assertIn('Divyansh Mishra', content)
        self.assertNotIn('Rohit Verma', content)
