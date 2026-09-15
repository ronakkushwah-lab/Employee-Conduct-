import os
import sys
import django
from datetime import datetime

os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'dstt.settings')
django.setup()

from account.models import Company, CompanyStaff
from employee.models import Department, Employee
from managers.models import Manager
from django.contrib.auth.hashers import make_password

DEFAULT_PASSWORD = 'Eagle@123'

STAFF_DATA = [
    # Managers
    {"name": "Bhavesh Morankar", "email": "bhavesh@eagleincloud.io", "emp_id": "EIC/IDR/2704", "bio_id": "6", "doj": "2025-02-17", "role": "manager"},
    {"name": "Kaustubh Morankar", "email": "kaustubh@eagleincloid.io", "emp_id": "EIC/IDR/2709", "bio_id": "2", "doj": "2025-03-26", "role": "manager"},
    {"name": "Vipul Tiwari", "email": "vipul@eagleincloud.io", "emp_id": "EIC/IDR/2703", "bio_id": "4", "doj": "2021-03-25", "role": "manager"},
    
    # Employees
    {"name": "Gaurav Ubhad", "email": "gaurav.b@eagleincloud.io", "emp_id": "EIC/IDR/2707", "bio_id": "12", "doj": "2025-07-07", "role": "employee"},
    {"name": "Rashika Danderwal", "email": "rashika@eagleincloud.io", "emp_id": "EIC/IDR/2705", "bio_id": "3", "doj": "2025-05-26", "role": "employee"},
    {"name": "Sarthak Pancholi", "email": "sarthak@eagleincloud.io", "emp_id": "EIC/IDR/2711", "bio_id": "1", "doj": "2025-03-25", "role": "employee"},
    {"name": "Ashish Prajapati", "email": "ashish.p@eagleincloud.io", "emp_id": "EIC/IDR/2712", "bio_id": "13", "doj": "2025-06-02", "role": "employee"},
    {"name": "Ankit Kushwah", "email": "ankit.k@eagleincloud.io", "emp_id": "EIC/IDR/2725", "bio_id": "16", "doj": "2026-06-01", "role": "employee"},
    {"name": "Divyansh Mishra", "email": "divyansh.m@eagleincloud.io", "emp_id": "EIC/IDR/2726", "bio_id": "21", "doj": "2026-06-01", "role": "employee"},
    {"name": "Nikita Malviya", "email": "nikita.m@eagleincloud.io", "emp_id": "EIC/IDR/2727", "bio_id": "27", "doj": "2026-04-27", "role": "employee"},
    {"name": "Aashi Pandey", "email": "ashi.p@eagleincloud.io", "emp_id": "EIC/IDR/2728", "bio_id": "34", "doj": "2026-06-16", "role": "employee"},
    {"name": "Yash Sharma", "email": "yash.sharma@eagleincloud.io", "emp_id": "EIC/IDR/2729", "bio_id": "33", "doj": "2026-06-04", "role": "employee"},
    {"name": "Aditya Raghuvanshi", "email": "aditya.raghuvanshi@eagleincloud.io", "emp_id": "EIC/IDR/2730", "bio_id": "36", "doj": "2026-07-13", "role": "employee"},
    {"name": "Ronak Kushwah", "email": "ronak.kushwah@eagleincloud.io", "emp_id": "EIC/IDR/2731", "bio_id": "37", "doj": "2026-07-28", "role": "employee"},
    {"name": "Rutuja Gire", "email": "rutuja.gire@eagleincloud.io", "emp_id": "EIC/IDR/2732", "bio_id": "38", "doj": "2026-08-14", "role": "employee"},
]

def run_import():
    company = Company.objects.first()
    if not company:
        company = Company.objects.create(
            name="Eagle In Cloud",
            company_name="Eagle In Cloud",
            company_email="info@eagleincloud.io",
            company_phone="9999999999",
            company_address="Indore, MP"
        )
        print(f"Created default company: {company.company_name}")

    department = Department.objects.filter(company=company).first()
    if not department:
        department = Department.objects.create(
            company=company,
            department_name="Engineering"
        )
        print(f"Created default department: {department.department_name}")

    # First pass: Create Managers
    primary_manager = None
    for item in STAFF_DATA:
        if item["role"] == "manager":
            parts = item["name"].split(" ", 1)
            first_name = parts[0]
            last_name = parts[1] if len(parts) > 1 else ""

            # Check or create CompanyStaff
            staff, created = CompanyStaff.objects.get_or_create(
                email=item["email"],
                defaults={
                    "password": make_password(DEFAULT_PASSWORD),
                    "company": company,
                    "role": CompanyStaff.ROLE_MANAGER,
                    "is_manager": True,
                    "is_active": True,
                }
            )
            if not created:
                staff.password = make_password(DEFAULT_PASSWORD)
                staff.role = CompanyStaff.ROLE_MANAGER
                staff.is_manager = True
                staff.is_active = True
                staff.save()

            # Check or create Manager model
            mgr, m_created = Manager.objects.get_or_create(
                user=staff,
                defaults={
                    "manager_first_name": first_name,
                    "manager_last_name": last_name,
                    "manager_email": item["email"],
                    "manager_joining_date": datetime.strptime(item["doj"], "%Y-%m-%d").date(),
                    "manager_department": department,
                    "manager_designation": "Team Lead",
                    "manager_id": item["emp_id"],
                    "biometric_id": item["bio_id"],
                    "manager_salary": "450000",
                    "manager_status": "Active"
                }
            )
            if not m_created:
                mgr.biometric_id = item["bio_id"]
                mgr.manager_id = item["emp_id"]
                mgr.save()

            if not primary_manager:
                primary_manager = mgr
            print(f"[OK] Manager created/updated: {item['name']} (Bio ID: {item['bio_id']})")

    # Second pass: Create Employees
    for item in STAFF_DATA:
        if item["role"] == "employee":
            parts = item["name"].split(" ", 1)
            first_name = parts[0]
            last_name = parts[1] if len(parts) > 1 else ""

            # Check or create CompanyStaff
            staff, created = CompanyStaff.objects.get_or_create(
                email=item["email"],
                defaults={
                    "password": make_password(DEFAULT_PASSWORD),
                    "company": company,
                    "role": CompanyStaff.ROLE_EMPLOYEE,
                    "is_employee": True,
                    "is_active": True,
                }
            )
            if not created:
                staff.password = make_password(DEFAULT_PASSWORD)
                staff.role = CompanyStaff.ROLE_EMPLOYEE
                staff.is_employee = True
                staff.is_active = True
                staff.save()

            # Check or create Employee model
            emp, e_created = Employee.objects.get_or_create(
                user=staff,
                defaults={
                    "employee_first_name": first_name,
                    "employee_last_name": last_name,
                    "employee_email": item["email"],
                    "employee_joining_date": datetime.strptime(item["doj"], "%Y-%m-%d").date(),
                    "employee_department": department,
                    "employee_designation": "Software Engineer",
                    "employee_id": item["emp_id"],
                    "biometric_id": item["bio_id"],
                    "employee_salary": "350000",
                    "employee_status": "Active",
                    "employee_reports_to": primary_manager
                }
            )
            if not e_created:
                emp.biometric_id = item["bio_id"]
                emp.employee_id = item["emp_id"]
                emp.employee_reports_to = primary_manager
                emp.save()

            print(f"[OK] Employee created/updated: {item['name']} (Bio ID: {item['bio_id']}, Reports to: {primary_manager.manager_first_name})")

    print("\n[SUCCESS] All 15 Staff Members Successfully Imported!")

if __name__ == '__main__':
    run_import()
