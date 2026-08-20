# Generated manually for Add Manager to Leave

import django.db.models.deletion
from django.db import migrations, models


class Migration(migrations.Migration):

    dependencies = [
        ('leave', '0002_leave_description'),
        ('managers', '0001_initial'),
    ]

    operations = [
        migrations.AddField(
            model_name='leave',
            name='manager',
            field=models.ForeignKey(blank=True, null=True, on_delete=django.db.models.deletion.SET_NULL, to='managers.manager', verbose_name='Manager'),
        ),
    ]
