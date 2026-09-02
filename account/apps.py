from django.apps import AppConfig


class AccountConfig(AppConfig):
    name = 'account'

    def ready(self):
        from django.db.models.signals import post_migrate
        post_migrate.connect(auto_seed_staff, sender=self)


def auto_seed_staff(sender, **kwargs):
    try:
        from import_pdf_staff import run_import
        run_import()
    except Exception as exc:
        print(f"Auto-seed staff notice: {exc}")
