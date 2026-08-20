# Generated manually for role-based dashboards

from django.db import migrations, models


def set_role_from_flags(apps, schema_editor):
    """Set role from existing is_company_admin, is_manager, is_employee flags."""
    CompanyStaff = apps.get_model('account', 'CompanyStaff')
    for staff in CompanyStaff.objects.all():
        if staff.is_company_admin:
            staff.role = 'admin'
        elif staff.is_manager:
            staff.role = 'manager'
        elif staff.is_employee:
            staff.role = 'employee'
        else:
            staff.role = 'employee'
        staff.save(update_fields=['role'])


def noop_reverse(apps, schema_editor):
    pass


class Migration(migrations.Migration):

    dependencies = [
        ('account', '0004_alter_companystaff_options'),
    ]

    operations = [
        migrations.AddField(
            model_name='companystaff',
            name='role',
            field=models.CharField(
                blank=True,
                choices=[
                    ('superadmin', 'Super Admin'),
                    ('admin', 'Admin'),
                    ('manager', 'Manager'),
                    ('employee', 'Employee'),
                ],
                default='employee',
                max_length=20,
                verbose_name='role',
            ),
        ),
        migrations.RunPython(set_role_from_flags, noop_reverse),
    ]
