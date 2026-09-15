# Add Reporting to / Approving Manager for manager resignation

import django.db.models.deletion
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('manager_resign', '0003_remove_managerresign_staff_managerresign_user'),
        ('managers', '0001_initial'),
    ]

    operations = [
        migrations.AddField(
            model_name='managerresign',
            name='assigned_too',
            field=models.ForeignKey(
                blank=True,
                null=True,
                on_delete=django.db.models.deletion.CASCADE,
                related_name='manager_resignations_to_approve',
                to='managers.manager',
                verbose_name='Reporting to / Approving Manager',
            ),
        ),
    ]
