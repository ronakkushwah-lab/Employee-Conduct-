# Fix: add administration_task.assigned_to_id if missing (DB can be out of sync with migration history)

from django.db import migrations, connection


def add_assigned_to_id_if_missing(apps, schema_editor):
    with connection.cursor() as cursor:
        if connection.vendor == 'sqlite':
            cursor.execute("PRAGMA table_info(administration_task)")
            columns = [row[1] for row in cursor.fetchall()]
        elif connection.vendor in ('postgresql', 'postgres'):
            cursor.execute(
                "SELECT column_name FROM information_schema.columns WHERE table_name = 'administration_task'"
            )
            columns = [row[0] for row in cursor.fetchall()]
        else:
            columns = []

        if columns and "assigned_to_id" not in columns:
            cursor.execute(
                "ALTER TABLE administration_task ADD COLUMN assigned_to_id INTEGER NULL REFERENCES employee_employee(id)"
            )


def noop_reverse(apps, schema_editor):
    pass


class Migration(migrations.Migration):

    dependencies = [
        ('administration', '0011_remove_asign_manager_staff_remove_asign_staff_and_more'),
        ('employee', '0006_remove_attendance_employee_and_more'),
    ]

    operations = [
        migrations.RunPython(add_assigned_to_id_if_missing, noop_reverse),
    ]
