from django.db import migrations
from django.contrib.auth.hashers import make_password


def seed_hr_account(apps, schema_editor):
    CompanyStaff = apps.get_model('account', 'CompanyStaff')
    Company = apps.get_model('account', 'Company')

    company = Company.objects.first()
    hashed_pwd = make_password('123456')

    # Seed/update HR account
    hr_staff, created = CompanyStaff.objects.get_or_create(
        email='hr@eagleincloud.io',
        defaults={
            'company': company,
            'password': hashed_pwd,
            'role': 'hr',
            'is_hr': True,
            'is_active': True,
            'is_authenticated': True,
        }
    )
    if not created:
        hr_staff.password = hashed_pwd
        hr_staff.role = 'hr'
        hr_staff.is_hr = True
        hr_staff.is_active = True
        hr_staff.save()

    # Also set password for ronak.kushwah@eagleincloud.io
    ronak = CompanyStaff.objects.filter(email='ronak.kushwah@eagleincloud.io').first()
    if ronak:
        ronak.password = hashed_pwd
        ronak.save()


class Migration(migrations.Migration):

    dependencies = [
        ('account', '0006_companystaff_is_hr_alter_companystaff_role'),
    ]

    operations = [
        migrations.RunPython(seed_hr_account, migrations.RunPython.noop),
    ]
