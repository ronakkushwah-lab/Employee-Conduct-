"""
Unit tests for Leave and Resignation workflows.
Tests: manager-only approval (<=2 days), admin approval (>2 days),
wrong manager/employee approval (must fail), admin approving manager-approved leave.
"""
from datetime import timedelta

from django.test import TestCase
from django.utils import timezone

from account.models import Company, CompanyStaff
from account.permissions import staff_can_approve_leave, get_role, is_admin, is_manager, is_employee
from leave.models import Leave
from resign.models import Resign


def make_date(days_from_today=0):
    return timezone.now().date() + timedelta(days=days_from_today)


class LeaveWorkflowTestCase(TestCase):
    """Leave approval: <=2 days (manager final), >2 days (manager then admin)."""

    def setUp(self):
        self.company = Company.objects.create(
            name='Test Co',
            company_name='Test Company',
            company_email='test@example.com',
        )
        # Admin
        self.admin = CompanyStaff.objects.create(
            company=self.company,
            email='admin@test.com',
            full_name='Admin User',
            role='admin',
            is_company_admin=True,
            is_active=True,
            is_authenticated=True,
        )
        # Manager (will be assigned as leave approver)
        self.manager = CompanyStaff.objects.create(
            company=self.company,
            email='manager@test.com',
            full_name='Manager One',
            role='manager',
            is_manager=True,
            is_active=True,
            is_authenticated=True,
        )
        # Other manager (not assigned to the leave)
        self.other_manager = CompanyStaff.objects.create(
            company=self.company,
            email='manager2@test.com',
            full_name='Manager Two',
            role='manager',
            is_manager=True,
            is_active=True,
            is_authenticated=True,
        )
        # Employee (reports to manager)
        self.employee = CompanyStaff.objects.create(
            company=self.company,
            email='employee@test.com',
            full_name='Employee User',
            role='employee',
            is_employee=True,
            is_active=True,
            is_authenticated=True,
            reporting_manager=self.manager,
        )

    def test_leave_leq_2_days_manager_approval_final(self):
        """Employee applying leave <= 2 days: manager approval is final (status = admin_approved)."""
        start = make_date(7)
        end = make_date(8)  # 2 days
        leave = Leave.objects.create(
            staff=self.employee,
            manager_staff=self.manager,
            startdate=start,
            enddate=end,
            leavetype='sick',
            reason='Sick',
            status=Leave.STATUS_PENDING,
        )
        self.assertEqual(leave.leave_days, 2)
        self.assertFalse(leave.needs_admin_approval)

        leave.approve_by_manager(self.manager)

        self.assertEqual(leave.status, Leave.STATUS_ADMIN_APPROVED)
        self.assertTrue(leave.is_approved)
        self.assertEqual(leave.manager_staff_id, self.manager.pk)

    def test_leave_gt_2_days_requires_admin_approval(self):
        """Employee applying leave > 2 days: needs_admin_approval; manager then admin must approve."""
        start = make_date(7)
        end = make_date(11)  # 5 days
        leave = Leave.objects.create(
            staff=self.employee,
            manager_staff=self.manager,
            startdate=start,
            enddate=end,
            leavetype='casual',
            reason='Trip',
            status=Leave.STATUS_PENDING,
        )
        self.assertGreater(leave.leave_days, 2)
        self.assertTrue(leave.needs_admin_approval)

        leave.approve_by_manager(self.manager)

        self.assertEqual(leave.status, Leave.STATUS_MANAGER_APPROVED)
        self.assertFalse(leave.is_approved)

        leave.approve_by_admin(self.admin)

        self.assertEqual(leave.status, Leave.STATUS_ADMIN_APPROVED)
        self.assertTrue(leave.is_approved)
        self.assertEqual(leave.admin_staff_id, self.admin.pk)

    def test_manager_cannot_approve_leave_not_assigned_to_them(self):
        """Manager trying to approve leave assigned to another manager should fail (permission check)."""
        start = make_date(7)
        end = make_date(8)
        leave = Leave.objects.create(
            staff=self.employee,
            manager_staff=self.manager,  # assigned to self.manager
            startdate=start,
            enddate=end,
            leavetype='casual',
            reason='Test',
            status=Leave.STATUS_PENDING,
        )

        # Only the assigned manager can approve (staff_can_approve_leave)
        self.assertTrue(staff_can_approve_leave(leave, self.manager))
        self.assertFalse(staff_can_approve_leave(leave, self.other_manager))

        # other_manager calling approve_by_manager would still change DB (model doesn't enforce);
        # permission is enforced in the view via staff_can_approve_leave / is_manager + manager_staff_id check.
        # So we only assert the permission helper.
        self.assertFalse(staff_can_approve_leave(leave, self.other_manager))

    def test_employee_cannot_approve_leave(self):
        """Employee trying to approve leave should fail (staff_can_approve_leave returns False)."""
        start = make_date(7)
        end = make_date(8)
        leave = Leave.objects.create(
            staff=self.employee,
            manager_staff=self.manager,
            startdate=start,
            enddate=end,
            leavetype='casual',
            reason='Test',
            status=Leave.STATUS_PENDING,
        )

        self.assertFalse(staff_can_approve_leave(leave, self.employee))
        self.assertTrue(is_employee(self.employee))

    def test_admin_approving_manager_approved_leave(self):
        """Admin can approve leave that is already manager_approved and needs_admin_approval."""
        start = make_date(7)
        end = make_date(11)
        leave = Leave.objects.create(
            staff=self.employee,
            manager_staff=self.manager,
            startdate=start,
            enddate=end,
            leavetype='casual',
            reason='Trip',
            status=Leave.STATUS_PENDING,
        )
        leave.approve_by_manager(self.manager)
        self.assertEqual(leave.status, Leave.STATUS_MANAGER_APPROVED)

        self.assertTrue(staff_can_approve_leave(leave, self.admin))
        leave.approve_by_admin(self.admin)

        self.assertEqual(leave.status, Leave.STATUS_ADMIN_APPROVED)
        self.assertTrue(leave.is_approved)
        self.assertEqual(leave.admin_staff_id, self.admin.pk)

    def test_admin_approve_by_admin_raises_when_not_manager_approved(self):
        """approve_by_admin must raise if status is not manager_approved."""
        start = make_date(7)
        end = make_date(11)
        leave = Leave.objects.create(
            staff=self.employee,
            manager_staff=self.manager,
            startdate=start,
            enddate=end,
            leavetype='casual',
            reason='Trip',
            status=Leave.STATUS_PENDING,
        )
        with self.assertRaises(ValueError) as ctx:
            leave.approve_by_admin(self.admin)
        self.assertIn('manager_approved', str(ctx.exception))

    def test_admin_approve_by_admin_raises_when_not_needs_admin(self):
        """approve_by_admin must raise when needs_admin_approval is False."""
        start = make_date(7)
        end = make_date(8)
        leave = Leave.objects.create(
            staff=self.employee,
            manager_staff=self.manager,
            startdate=start,
            enddate=end,
            leavetype='casual',
            reason='Short',
            status=Leave.STATUS_PENDING,
        )
        leave.approve_by_manager(self.manager)
        self.assertEqual(leave.status, Leave.STATUS_ADMIN_APPROVED)
        self.assertFalse(leave.needs_admin_approval)

        leave.status = Leave.STATUS_MANAGER_APPROVED  # force to test admin path
        leave.needs_admin_approval = False
        leave.save()

        with self.assertRaises(ValueError) as ctx:
            leave.approve_by_admin(self.admin)
        self.assertIn('not required', str(ctx.exception))


class ResignWorkflowTestCase(TestCase):
    """Resignation: manager approves first, then admin."""

    def setUp(self):
        self.company = Company.objects.create(
            name='Resign Test Co',
            company_name='Resign Test',
            company_email='resign@test.com',
        )
        self.admin = CompanyStaff.objects.create(
            company=self.company,
            email='admin@resign.com',
            full_name='Admin',
            role='admin',
            is_company_admin=True,
            is_active=True,
            is_authenticated=True,
        )
        self.manager = CompanyStaff.objects.create(
            company=self.company,
            email='mgr@resign.com',
            full_name='Manager',
            role='manager',
            is_manager=True,
            is_active=True,
            is_authenticated=True,
        )
        self.employee = CompanyStaff.objects.create(
            company=self.company,
            email='emp@resign.com',
            full_name='Employee',
            role='employee',
            is_employee=True,
            is_active=True,
            is_authenticated=True,
            reporting_manager=self.manager,
        )

    def test_resign_manager_then_admin_approval(self):
        """Resignation: manager approval then admin approval."""
        resign = Resign.objects.create(
            staff=self.employee,
            startdate=make_date(14),
            reason='Personal',
            status=Resign.STATUS_PENDING,
        )
        self.assertFalse(resign.is_approved)

        resign.approve_by_manager(self.manager)
        self.assertEqual(resign.status, Resign.STATUS_MANAGER_APPROVED)
        self.assertEqual(resign.approved_by_staff_id, self.manager.pk)

        resign.approve_by_admin(self.admin)
        self.assertEqual(resign.status, Resign.STATUS_ADMIN_APPROVED)
        self.assertTrue(resign.is_approved)
        self.assertEqual(resign.admin_staff_id, self.admin.pk)

    def test_resign_admin_approve_raises_if_not_manager_approved(self):
        """Resign: approve_by_admin must raise if status is not manager_approved."""
        resign = Resign.objects.create(
            staff=self.employee,
            startdate=make_date(14),
            reason='Personal',
            status=Resign.STATUS_PENDING,
        )
        with self.assertRaises(ValueError) as ctx:
            resign.approve_by_admin(self.admin)
        self.assertIn('manager_approved', str(ctx.exception))
