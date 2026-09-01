from __future__ import unicode_literals
from django.contrib.auth.models import AbstractBaseUser, BaseUserManager, PermissionsMixin
from django.db import models
from django.contrib.auth.models import AbstractUser, Group
from django.utils.translation import gettext_lazy as _
from django.contrib.auth.models import BaseUserManager
from django.utils import timezone
from django.contrib.auth.models import User


# Group.add_to_class('employee_access_flag', models.BooleanField(default=True))

class UserManager(BaseUserManager):
    """
    Custom user model manager where email is the unique identifiers
    for authentication instead of usernames.
    """

    def create_user(self, email, password, **extra_fields):
        if not email:
            raise ValueError('Users must have an email address')
        now = timezone.now()
        email = self.normalize_email(email)
        user = self.model(
            email=email,
            last_login=now,
            date_joined=now,

            **extra_fields
        )

        user.set_password(password)
        user.save(using=self._db)
        return user

    def create_superuser(self, email, password, **extra_fields):
        """
        Create and save a SuperUser with the given email and password.
        """
        extra_fields.setdefault('is_staff', True)
        extra_fields.setdefault('is_superuser', True)
        extra_fields.setdefault('is_active', True)
        extra_fields.setdefault('is_admin', True)

        if extra_fields.get('is_staff') is not True:
            raise ValueError(_('Superuser must have is_staff=True.'))
        if extra_fields.get('is_superuser') is not True:
            raise ValueError(_('Superuser must have is_superuser=True.'))
        return self.create_user(email, password, **extra_fields)

    def __str__(self):
        return str(self.get_user)



class User(AbstractBaseUser, PermissionsMixin):
    username = None
    email = models.EmailField(_('email address'), unique=True)
    full_name = models.CharField(max_length=254, null=True, blank=True)
    is_admin = models.BooleanField(default=False)
    is_employee = models.BooleanField(default=False)
    is_staff = models.BooleanField(default=False)
    is_superuser = models.BooleanField(default=False)
    is_active = models.BooleanField(default=True)
    last_login = models.DateTimeField(null=True, blank=True)
    date_joined = models.DateTimeField(auto_now_add=True)
    USERNAME_FIELD = 'email'
    REQUIRED_FIELDS = []
    objects = UserManager()

    def get_absolute_url(self):
        return "/users/%i/" % (self.pk)


class Company(models.Model):
    id = models.AutoField(primary_key=True)
    name = models.CharField(max_length=100, null=True)
    company_name = models.CharField(max_length=100, null=True)
    company_email = models.EmailField(max_length=100, null=True,blank=True)
    company_phone = models.CharField(max_length=100, null=True)
    company_address = models.CharField(max_length=200, null=True)

    def __str__(self):
        return self.company_name


class CompanyStaff(models.Model):
    ROLE_SUPERADMIN = 'superadmin'
    ROLE_ADMIN = 'admin'
    ROLE_MANAGER = 'manager'
    ROLE_EMPLOYEE = 'employee'
    ROLE_CHOICES = [
        (ROLE_SUPERADMIN, _('Super Admin')),
        (ROLE_ADMIN, _('Admin')),
        (ROLE_MANAGER, _('Manager')),
        (ROLE_EMPLOYEE, _('Employee')),
    ]

    id = models.AutoField(primary_key=True)
    company = models.ForeignKey(Company,on_delete=models.CASCADE, null=True,blank=True)
    email = models.EmailField(_('email address'),null=True,blank=True)
    password = models.CharField(max_length=100,null=True,blank=True)
    role = models.CharField(
        _('role'),
        max_length=20,
        choices=ROLE_CHOICES,
        default=ROLE_EMPLOYEE,
        blank=True,
    )
    is_company_admin = models.BooleanField(default=False)
    is_manager = models.BooleanField(default=False)
    is_employee = models.BooleanField(default=False)
    is_active = models.BooleanField(default=True)
    is_authenticated = models.BooleanField(default=False)
    new_notification = models.BooleanField(default=False, null=False, blank=False)

    def __str__(self):
        return self.email

    @staticmethod
    def get_Staff_by_email(email):
        try:
            return CompanyStaff.objects.get(email=email)
        except:
            return False

    def isExists(self):
        if CompanyStaff.objects.filter(email=self.email):
            return True

        return False

    def save(self, *args, **kwargs):
        # Keep role in sync with legacy flags when role is not explicitly set
        if not self.role or self.role == self.ROLE_EMPLOYEE:
            if self.is_company_admin:
                self.role = self.ROLE_ADMIN
            elif self.is_manager:
                self.role = self.ROLE_MANAGER
            elif self.is_employee:
                self.role = self.ROLE_EMPLOYEE

        # Hash plain-text password if created/edited from Django Admin
        if self.password and not (self.password.startswith('pbkdf2_sha256$') or self.password.startswith('argon2') or self.password.startswith('bcrypt$')):
            from django.contrib.auth.hashers import make_password
            self.password = make_password(self.password)

        super().save(*args, **kwargs)




# request.user.email = email,  user=CompanyStaff.objevts.get(email=email), Employye.objects.get(user=user), employee=employee
