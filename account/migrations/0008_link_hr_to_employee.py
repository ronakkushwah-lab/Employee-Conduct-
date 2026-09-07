from django.db import migrations
from datetime import date


def link_hr_to_employee(apps, schema_editor):
    CompanyStaff = apps.get_model('account', 'CompanyStaff')
    Company = apps.get_model('account', 'Company')
    Employee = apps.get_model('employee', 'Employee')
    Department = apps.get_model('employee', 'Department')
    Manager = apps.get_model('managers', 'Manager')

    company = Company.objects.first()
    if not company:
        return

    # Find or create HR Department
    hr_dept, _ = Department.objects.get_or_create(
        company=company,
        department_name='Human Resources'
    )

    # Find an active manager to assign as HR's reporting manager
    reporting_manager = Manager.objects.filter(user__company=company).first() or Manager.objects.first()

    # Find all staff with HR role or is_hr=True
    hr_staffs = CompanyStaff.objects.filter(role='hr') | CompanyStaff.objects.filter(is_hr=True)

    for staff in hr_staffs:
        staff.is_employee = True
        staff.is_hr = True
        staff.role = 'hr'
        staff.save()

        emp = Employee.objects.filter(user=staff).first()
        if not emp:
            staff_id_num = staff.id
            emp_id_str = f"EIC/IDR/{2700 + staff_id_num}"
            bio_id_str = str(staff_id_num)
            if Employee.objects.filter(biometric_id=bio_id_str).exists():
                bio_id_str = f"BIO-{staff_id_num}"

            emp = Employee.objects.create(
                user=staff,
                employee_first_name='HR',
                employee_last_name='Manager',
                employee_email=staff.email or 'hr@eagleincloud.io',
                employee_joining_date=date.today(),
                employee_department=hr_dept,
                employee_designation='HR Manager',
                employee_id=emp_id_str,
                biometric_id=bio_id_str,
                employee_reports_to=reporting_manager,
                employee_salary='600000',
                employee_status='Active'
            )
        else:
            if not emp.employee_reports_to and reporting_manager:
                emp.employee_reports_to = reporting_manager
            if not emp.biometric_id:
                emp.biometric_id = str(staff.id)
            if not emp.employee_department:
                emp.employee_department = hr_dept
            emp.save()


class Migration(migrations.Migration):

    dependencies = [
        ('account', '0007_seed_hr_account'),
        ('employee', '0001_initial'),
        ('managers', '0001_initial'),
    ]

    operations = [
        migrations.RunPython(link_hr_to_employee, migrations.RunPython.noop),
    ]
