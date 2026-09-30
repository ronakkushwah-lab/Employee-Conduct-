import logging
from django.core.management.base import BaseCommand
from payroll.auto_payslip_service import generate_monthly_payslips, get_default_target_month

logger = logging.getLogger(__name__)


class Command(BaseCommand):
    help = 'Automatically generate monthly payslips for all active Employees and Managers and send email alerts.'

    def add_arguments(self, parser):
        parser.add_argument(
            '--month',
            type=str,
            help='Target month for salary in YYYY-MM format (e.g. 2026-09). Defaults to previous calendar month.',
        )
        parser.add_argument(
            '--company',
            type=int,
            help='Optional company ID filter.',
        )
        parser.add_argument(
            '--no-email',
            action='store_true',
            help='Skip sending automated email notifications to employees.',
        )
        parser.add_argument(
            '--force',
            action='store_true',
            help='Force overwrite existing salary records for the target month.',
        )

    def handle(self, *args, **options):
        target_month = options.get('month')
        company_id = options.get('company')
        send_email = not options.get('no_email', False)
        force = options.get('force', False)

        self.stdout.write(self.style.NOTICE(f"Initiating monthly payslip generation..."))
        
        result = generate_monthly_payslips(
            target_month=target_month,
            company_id=company_id,
            send_email=send_email,
            force=force
        )

        self.stdout.write(self.style.SUCCESS(
            f"Successfully generated payslips for {result['target_month_name']}!\n"
            f"  - Employees Created: {result['employees_created']} (Skipped: {result['employees_skipped']})\n"
            f"  - Managers Created: {result['managers_created']} (Skipped: {result['managers_skipped']})\n"
            f"  - Emails Sent: {result['emails_sent']}"
        ))
