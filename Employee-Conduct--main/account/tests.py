from datetime import timedelta
from django.test import TestCase, Client
from django.utils import timezone
from django.urls import reverse
from django.contrib.auth.hashers import make_password, check_password

from account.models import Company, CompanyStaff, User


class AccountModelAndAuthTests(TestCase):
    def setUp(self):
        self.client = Client()
        self.company = Company.objects.create(
            company_name='Eagle In Cloud Test',
            name='Eagle In Cloud Test',
            company_email='info@eagleinclouds.com'
        )
        self.staff_admin = CompanyStaff.objects.create(
            company=self.company,
            email='admin@eagleincloud.test',
            password=make_password('SecurePass123!'),
            role=CompanyStaff.ROLE_ADMIN,
            is_company_admin=True,
            is_authenticated=True,
            password_changed_at=timezone.now()
        )
        self.staff_employee = CompanyStaff.objects.create(
            company=self.company,
            email='emp@eagleincloud.test',
            password=make_password('SecurePass123!'),
            role=CompanyStaff.ROLE_EMPLOYEE,
            is_employee=True,
            is_authenticated=True,
            password_changed_at=timezone.now()
        )

    def test_company_creation(self):
        """Test Company model creation and string representation."""
        self.assertEqual(str(self.company), 'Eagle In Cloud Test')
        self.assertEqual(self.company.company_email, 'info@eagleinclouds.com')

    def test_company_staff_roles(self):
        """Test staff role assignment and multi-tenant mapping."""
        self.assertEqual(self.staff_admin.role, CompanyStaff.ROLE_ADMIN)
        self.assertTrue(self.staff_admin.is_company_admin)
        self.assertEqual(self.staff_employee.role, CompanyStaff.ROLE_EMPLOYEE)
        self.assertTrue(self.staff_employee.is_employee)
        self.assertEqual(self.staff_admin.company, self.company)

    def test_60_day_password_expiry_policy(self):
        """Test that passwords older than 60 days are marked as expired."""
        # Fresh password: not expired
        self.assertFalse(self.staff_admin.is_password_expired(expiry_days=60))

        # Password changed 61 days ago: expired
        self.staff_admin.password_changed_at = timezone.now() - timedelta(days=61)
        self.staff_admin.save()
        self.assertTrue(self.staff_admin.is_password_expired(expiry_days=60))

        # Password changed 30 days ago: not expired
        self.staff_admin.password_changed_at = timezone.now() - timedelta(days=30)
        self.staff_admin.save()
        self.assertFalse(self.staff_admin.is_password_expired(expiry_days=60))

    def test_login_page_renders(self):
        """Test that the login view renders successfully."""
        response = self.client.get('/login/')
        self.assertIn(response.status_code, [200, 302])

    def test_invalid_login_credentials_rejected(self):
        """Test that invalid credentials are not authenticated."""
        response = self.client.post('/login/', {
            'username': 'fake_user_123@test.com',
            'password': 'WrongPassword999!'
        })
        self.assertIn(response.status_code, [200, 302])
        # User should not have active company_staff_id session
        self.assertNotIn('company_staff_id', self.client.session)

    def test_forgot_password_view_post_handling(self):
        """Test forgotpass endpoint gracefully handles submission."""
        response = self.client.post('/forgotpass/', {
            'email': self.staff_admin.email,
            'password': 'NewPassword12345!'
        })
        self.assertIn(response.status_code, [200, 302])
        # Verify password hash was updated
        updated_staff = CompanyStaff.objects.get(pk=self.staff_admin.pk)
        self.assertTrue(check_password('NewPassword12345!', updated_staff.password))


