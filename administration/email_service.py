"""
Gmail IMAP Email Fetching Service
Connects to Gmail via IMAP and fetches emails into EmailNotification model.
"""
import imaplib
import email
from email.header import decode_header
from email.utils import parsedate_to_datetime, parseaddr
from datetime import datetime, timedelta

from django.conf import settings
from django.utils import timezone

from .models import EmailNotification


def decode_mime_header(header_value):
    """Decode a MIME-encoded email header into a readable string."""
    if not header_value:
        return ''
    decoded_parts = decode_header(header_value)
    result = []
    for part, charset in decoded_parts:
        if isinstance(part, bytes):
            try:
                result.append(part.decode(charset or 'utf-8', errors='replace'))
            except (LookupError, UnicodeDecodeError):
                result.append(part.decode('utf-8', errors='replace'))
        else:
            result.append(part)
    return ''.join(result)


def get_email_body(msg):
    """Extract the text body from an email message."""
    body = ''
    if msg.is_multipart():
        for part in msg.walk():
            content_type = part.get_content_type()
            content_disposition = str(part.get('Content-Disposition', ''))
            if content_type == 'text/plain' and 'attachment' not in content_disposition:
                try:
                    charset = part.get_content_charset() or 'utf-8'
                    body = part.get_payload(decode=True).decode(charset, errors='replace')
                except Exception:
                    body = str(part.get_payload(decode=True))
                break
        # If no plain text found, try HTML
        if not body:
            for part in msg.walk():
                content_type = part.get_content_type()
                content_disposition = str(part.get('Content-Disposition', ''))
                if content_type == 'text/html' and 'attachment' not in content_disposition:
                    try:
                        charset = part.get_content_charset() or 'utf-8'
                        raw_html = part.get_payload(decode=True).decode(charset, errors='replace')
                        # Strip HTML tags for preview
                        import re
                        body = re.sub(r'<[^>]+>', ' ', raw_html)
                        body = re.sub(r'\s+', ' ', body).strip()
                    except Exception:
                        body = ''
                    break
    else:
        content_type = msg.get_content_type()
        try:
            charset = msg.get_content_charset() or 'utf-8'
            payload = msg.get_payload(decode=True)
            if payload:
                body = payload.decode(charset, errors='replace')
                if content_type == 'text/html':
                    import re
                    body = re.sub(r'<[^>]+>', ' ', body)
                    body = re.sub(r'\s+', ' ', body).strip()
        except Exception:
            body = ''
    return body[:2000]  # Limit preview to 2000 chars


def fetch_emails_from_gmail(company=None, max_emails=50, days_back=30):
    """
    Fetch emails from Gmail inbox via IMAP and store them as EmailNotification objects.
    
    Args:
        company: Company instance to associate notifications with (optional)
        max_emails: Maximum number of emails to fetch
        days_back: How many days back to look for emails
    
    Returns:
        dict with 'new_count', 'total_fetched', 'errors'
    """
    result = {'new_count': 0, 'total_fetched': 0, 'errors': []}
    
    imap_host = getattr(settings, 'GMAIL_IMAP_HOST', 'imap.gmail.com')
    imap_port = getattr(settings, 'GMAIL_IMAP_PORT', 993)
    email_address = getattr(settings, 'GMAIL_EMAIL', '')
    app_password = getattr(settings, 'GMAIL_APP_PASSWORD', '')
    
    if not email_address or not app_password:
        result['errors'].append('Gmail credentials not configured in settings.')
        return result

    mail = None
    try:
        # Connect to Gmail IMAP
        mail = imaplib.IMAP4_SSL(imap_host, imap_port)
        mail.login(email_address, app_password)
        mail.select('INBOX')
        
        # Search for emails from the last N days
        since_date = (datetime.now() - timedelta(days=days_back)).strftime('%d-%b-%Y')
        status, messages_data = mail.search(None, f'(SINCE {since_date})')
        
        if status != 'OK':
            result['errors'].append('Failed to search emails.')
            return result
        
        email_ids = messages_data[0].split()
        if not email_ids:
            return result
        
        # Take the most recent emails (up to max_emails)
        email_ids = email_ids[-max_emails:]
        result['total_fetched'] = len(email_ids)
        
        for eid in email_ids:
            try:
                status, msg_data = mail.fetch(eid, '(RFC822)')
                if status != 'OK':
                    continue
                
                raw_email = msg_data[0][1]
                msg = email.message_from_bytes(raw_email)
                
                # Extract Message-ID
                message_id = msg.get('Message-ID', '')
                if not message_id:
                    message_id = f'generated-{eid.decode()}-{msg.get("Date", "")}'
                
                # Skip if already stored
                if EmailNotification.objects.filter(message_id=message_id).exists():
                    continue
                
                # Extract sender info
                sender_raw = msg.get('From', '')
                sender_name, sender_email_addr = parseaddr(sender_raw)
                sender_name = decode_mime_header(sender_name)
                
                # Extract subject
                subject = decode_mime_header(msg.get('Subject', ''))
                if not subject:
                    subject = '(No Subject)'
                
                # Extract date
                date_str = msg.get('Date', '')
                received_date = None
                if date_str:
                    try:
                        received_date = parsedate_to_datetime(date_str)
                        if timezone.is_naive(received_date):
                            received_date = timezone.make_aware(received_date)
                    except Exception:
                        received_date = timezone.now()
                else:
                    received_date = timezone.now()
                
                # Extract body preview
                body_preview = get_email_body(msg)
                
                # Save to database
                EmailNotification.objects.create(
                    company=company,
                    message_id=message_id,
                    sender_email=sender_email_addr[:254],
                    sender_name=sender_name[:254],
                    subject=subject[:500],
                    body_preview=body_preview,
                    received_date=received_date,
                    is_read=False,
                )
                result['new_count'] += 1
                
            except Exception as e:
                result['errors'].append(f'Error processing email {eid}: {str(e)}')
                continue
    
    except imaplib.IMAP4.error as e:
        result['errors'].append(f'IMAP authentication/connection error: {str(e)}')
    except Exception as e:
        result['errors'].append(f'Unexpected error: {str(e)}')
    finally:
        if mail:
            try:
                mail.close()
                mail.logout()
            except Exception:
                pass
    
    return result
