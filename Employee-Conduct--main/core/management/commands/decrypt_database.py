"""
Management command to reverse encryption and decrypt all database records back to plain text.
Usage: python manage.py decrypt_database
"""
from django.core.management.base import BaseCommand
from django.db import connection, transaction
from employee.models import Employee
from managers.models import Manager
from payroll.models import Salary as EmployeeSalary
from managerpayroll.models import Salary as ManagerSalary
from core.encryption import decrypt_value


class Command(BaseCommand):
    help = 'Reverses encryption and restores all database fields back to plain text'

    def handle(self, *args, **options):
        self.stdout.write(self.style.WARNING('Starting database decryption and plain-text restoration...'))

        with transaction.atomic():
            with connection.cursor() as cursor:
                # Decrypt Employee fields directly in raw SQL to write pure plain-text
                emp_fields = [
                    'employee_phone', 'employee_salary', 'employee_birth_date',
                    'employee_address', 'employee_pin_code', 'employee_tel',
                    'employee_emergency_primary_phone1', 'employee_emergency_primary_phone2'
                ]
                for emp in Employee.objects.all():
                    updates = {}
                    for f in emp_fields:
                        val = getattr(emp, f, None)
                        if val:
                            updates[f] = decrypt_value(str(val))
                    if updates:
                        set_clause = ", ".join([f'"{k}" = %s' for k in updates.keys()])
                        cursor.execute(f'UPDATE "employee_employee" SET {set_clause} WHERE id = %s', list(updates.values()) + [emp.id])

                # Decrypt Manager fields directly
                mgr_fields = [
                    'manager_phone', 'manager_salary', 'manager_birth_date',
                    'manager_address', 'manager_pin_code', 'manager_tel',
                    'manager_emergency_primary_phone1', 'manager_emergency_primary_phone2'
                ]
                for mgr in Manager.objects.all():
                    updates = {}
                    for f in mgr_fields:
                        val = getattr(mgr, f, None)
                        if val:
                            updates[f] = decrypt_value(str(val))
                    if updates:
                        set_clause = ", ".join([f'"{k}" = %s' for k in updates.keys()])
                        cursor.execute(f'UPDATE "managers_manager" SET {set_clause} WHERE id = %s', list(updates.values()) + [mgr.id])

                # Decrypt Employee Salary fields
                sal_fields = [
                    'basic', 'da_percent', 'hra_percent', 'conveyance', 'bonuses',
                    'allowance', 'medical_allowance', 'tds', 'esi', 'providence_fund',
                    'leave', 'tax', 'labour_welfare', 'loan_repayment', 'others'
                ]
                for sal in EmployeeSalary.objects.all():
                    updates = {}
                    for f in sal_fields:
                        val = getattr(sal, f, None)
                        if val is not None:
                            try:
                                updates[f] = int(decrypt_value(str(val)))
                            except Exception:
                                updates[f] = 0
                    if updates:
                        set_clause = ", ".join([f'"{k}" = %s' for k in updates.keys()])
                        cursor.execute(f'UPDATE "payroll_salary" SET {set_clause} WHERE id = %s', list(updates.values()) + [sal.id])

                # Decrypt Manager Salary fields
                for msal in ManagerSalary.objects.all():
                    updates = {}
                    for f in sal_fields:
                        val = getattr(msal, f, None)
                        if val is not None:
                            try:
                                updates[f] = int(decrypt_value(str(val)))
                            except Exception:
                                updates[f] = 0
                    if updates:
                        set_clause = ", ".join([f'"{k}" = %s' for k in updates.keys()])
                        cursor.execute(f'UPDATE "managerpayroll_salary" SET {set_clause} WHERE id = %s', list(updates.values()) + [msal.id])

        self.stdout.write(self.style.SUCCESS('Database decrypted successfully back to plain text!'))
