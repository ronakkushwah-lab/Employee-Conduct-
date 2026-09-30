"""
Management command to encrypt existing plain-text PII & Financial records in the database.
Usage: python manage.py encrypt_existing_data
"""
from django.core.management.base import BaseCommand
from django.db import transaction
from employee.models import Employee
from managers.models import Manager
from payroll.models import Salary as EmployeeSalary
from managerpayroll.models import Salary as ManagerSalary
from core.encryption import encrypt_value


class Command(BaseCommand):
    help = 'Encrypts all existing plain-text Employee, Manager, and Salary records using AES-256 Fernet'

    def handle(self, *args, **options):
        self.stdout.write(self.style.NOTICE('Starting database encryption process...'))

        with transaction.atomic():
            # 1. Encrypt Employee PII
            emp_count = 0
            for emp in Employee.objects.all():
                emp.save()
                emp_count += 1
            self.stdout.write(self.style.SUCCESS(f'Successfully encrypted {emp_count} Employee records.'))

            # 2. Encrypt Manager PII
            mgr_count = 0
            for mgr in Manager.objects.all():
                mgr.save()
                mgr_count += 1
            self.stdout.write(self.style.SUCCESS(f'Successfully encrypted {mgr_count} Manager records.'))

            # 3. Encrypt Employee Salaries
            esal_count = 0
            for sal in EmployeeSalary.objects.all():
                sal.save()
                esal_count += 1
            self.stdout.write(self.style.SUCCESS(f'Successfully encrypted {esal_count} Employee Salary records.'))

            # 4. Encrypt Manager Salaries
            msal_count = 0
            for msal in ManagerSalary.objects.all():
                msal.save()
                msal_count += 1
            self.stdout.write(self.style.SUCCESS(f'Successfully encrypted {msal_count} Manager Salary records.'))

        self.stdout.write(self.style.SUCCESS('All database PII & Financial records encrypted successfully!'))
