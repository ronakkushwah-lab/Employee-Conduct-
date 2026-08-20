"""
Django management command to fetch emails from Gmail via IMAP.

Usage:
    python manage.py fetch_emails
    python manage.py fetch_emails --max-emails 100 --days-back 7
    python manage.py fetch_emails --company-id 1
"""
from django.core.management.base import BaseCommand
from account.models import Company
from administration.email_service import fetch_emails_from_gmail


class Command(BaseCommand):
    help = 'Fetch emails from Gmail inbox via IMAP and store as notifications'

    def add_arguments(self, parser):
        parser.add_argument(
            '--max-emails',
            type=int,
            default=50,
            help='Maximum number of emails to fetch (default: 50)'
        )
        parser.add_argument(
            '--days-back',
            type=int,
            default=30,
            help='How many days back to look for emails (default: 30)'
        )
        parser.add_argument(
            '--company-id',
            type=int,
            default=None,
            help='Company ID to associate emails with (optional)'
        )

    def handle(self, *args, **options):
        max_emails = options['max_emails']
        days_back = options['days_back']
        company_id = options['company_id']

        company = None
        if company_id:
            try:
                company = Company.objects.get(id=company_id)
                self.stdout.write(f'Associating emails with company: {company}')
            except Company.DoesNotExist:
                self.stderr.write(self.style.ERROR(f'Company with ID {company_id} not found.'))
                return

        self.stdout.write(f'Fetching emails (max: {max_emails}, days back: {days_back})...')
        
        result = fetch_emails_from_gmail(
            company=company,
            max_emails=max_emails,
            days_back=days_back
        )

        if result['new_count'] > 0:
            self.stdout.write(self.style.SUCCESS(
                f"Successfully fetched {result['new_count']} new email(s) "
                f"(out of {result['total_fetched']} checked)."
            ))
        else:
            self.stdout.write(f"No new emails found ({result['total_fetched']} checked).")

        if result['errors']:
            for error in result['errors']:
                self.stderr.write(self.style.WARNING(f'Warning: {error}'))
