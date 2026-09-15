from datetime import date, timedelta

from django.test import TestCase

from account.models import Company, CompanyStaff
from employee.models import Department, Employee
from leave.models import BalanceLeaves, Leave
from managers.models import Manager
from resign.models import Resign


class CurrentLeaveWorkflowTests(TestCase):
    def setUp(self):
        company = Company.objects.create(company_name='Workflow Co')
        department = Department.objects.create(company=company, department_name='Ops')
        manager_staff = CompanyStaff.objects.create(
            company=company, email='manager@workflow.test', password='pass', is_manager=True
        )
        self.manager = Manager.objects.create(
            user=manager_staff,
            manager_first_name='Test', manager_last_name='Manager',
            manager_email=manager_staff.email,
            manager_joining_date=date(2025, 1, 1),
            manager_department=department,
            manager_designation='Manager', manager_id='M-1',
        )
        employee_staff = CompanyStaff.objects.create(
            company=company, email='employee@workflow.test', password='pass', is_employee=True
        )
        self.employee = Employee.objects.create(
            user=employee_staff,
            employee_first_name='Test', employee_last_name='Employee',
            employee_email=employee_staff.email,
            employee_joining_date=date(2025, 1, 2),
            employee_department=department,
            employee_designation='Analyst', employee_id='E-1',
            employee_reports_to=self.manager,
        )

    def make_leave(self, days=2):
        start = date.today() + timedelta(days=7)
        return Leave.objects.create(
            user=self.employee, manager=self.manager,
            startdate=start, enddate=start + timedelta(days=days - 1),
            leavetype='casual', reason='Test',
        )

    def test_leave_state_transitions(self):
        leave = self.make_leave()
        self.assertEqual(leave.leave_days, 2)
        leave.approve_by_manager
        self.assertEqual(leave.status, 'pending_hr')
        leave.approve_leave
        self.assertEqual(leave.status, 'approved')
        self.assertTrue(leave.is_approved)
        leave.reject_leave
        self.assertEqual(leave.status, 'rejected')
        self.assertFalse(leave.is_approved)

    def test_leave_balance_excludes_rejected_requests(self):
        BalanceLeaves.objects.create(user=self.employee, balancedays=10)
        active = self.make_leave(days=3)
        rejected = self.make_leave(days=4)
        rejected.reject_leave
        summary = BalanceLeaves.get_balance_summary(self.employee)
        self.assertEqual(summary['used_days'], active.leave_days)
        self.assertEqual(summary['remaining_balance'], 7)

    def test_resignation_state_transitions(self):
        resignation = Resign.objects.create(
            user=self.employee, assigned_too=self.manager,
            startdate=date.today() + timedelta(days=14), reason='Personal',
        )
        resignation.approve_resign
        self.assertEqual(resignation.status, 'approved')
        resignation.unapprove_resign
        self.assertEqual(resignation.status, 'pending')
        resignation.resign_cancel
        self.assertEqual(resignation.status, 'cancelled')
        resignation.reject_resign
        self.assertEqual(resignation.status, 'rejected')
