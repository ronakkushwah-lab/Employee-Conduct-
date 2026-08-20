from django.db import migrations, models
import django.db.models.deletion
import django.utils.timezone


class Migration(migrations.Migration):

    dependencies = [
        ("account", "0001_initial"),
        ("administration", "0012_fix_task_assigned_to_column"),
        ("managers", "0001_initial"),
    ]

    operations = [
        migrations.CreateModel(
            name="ManagerProject",
            fields=[
                ("id", models.BigAutoField(auto_created=True, primary_key=True, serialize=False, verbose_name="ID")),
                ("title", models.CharField(max_length=70)),
                ("description", models.TextField(blank=True, max_length=500, null=True)),
                ("created_date", models.DateField(blank=True, default=django.utils.timezone.now)),
                (
                    "assigned_to",
                    models.ForeignKey(
                        blank=True,
                        null=True,
                        on_delete=django.db.models.deletion.CASCADE,
                        related_name="admin_projects",
                        to="managers.manager",
                    ),
                ),
                (
                    "company",
                    models.ForeignKey(
                        blank=True,
                        default=True,
                        null=True,
                        on_delete=django.db.models.deletion.CASCADE,
                        to="account.company",
                    ),
                ),
                (
                    "created_by",
                    models.ForeignKey(
                        blank=True,
                        null=True,
                        on_delete=django.db.models.deletion.SET_NULL,
                        to="account.companystaff",
                    ),
                ),
            ],
        ),
    ]

