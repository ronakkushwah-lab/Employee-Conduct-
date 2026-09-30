import os
import logging
from datetime import date, datetime
from dateutil.relativedelta import relativedelta
from django.conf import settings
from django.core.mail import EmailMultiAlternatives
from django.utils.html import strip_tags
from django.utils import timezone

logger = logging.getLogger(__name__)


def get_default_target_month(ref_date=None):
    """
    Returns the first day of the previous calendar month.
    e.g., on 2026-10-10 -> 2026-09-01
    """
    if ref_date is None:
        ref_date = date.today()
    elif isinstance(ref_date, datetime):
        ref_date = ref_date.date()
    
    first_of_current = date(ref_date.year, ref_date.month, 1)
    prev_month = first_of_current - relativedelta(months=1)
    return date(prev_month.year, prev_month.month, 1)


def send_payslip_email(salary_obj, is_manager=False):
    """
    Sends a beautifully formatted payslip notification email to employee/manager.
    """
    try:
        if is_manager:
            person = salary_obj.manager
            first_name = person.manager_first_name if hasattr(person, 'manager_first_name') else ''
            last_name = person.manager_last_name if hasattr(person, 'manager_last_name') else ''
            recipient_email = person.manager_email or (person.user.email if person.user else '')
            role_label = 'Manager'
        else:
            person = salary_obj.employee
            first_name = person.employee_first_name if hasattr(person, 'employee_first_name') else ''
            last_name = person.employee_last_name if hasattr(person, 'employee_last_name') else ''
            recipient_email = person.employee_email or (person.user.email if person.user else '')
            role_label = 'Employee'

        if not recipient_email or '@' not in recipient_email:
            logger.warning(f"No valid email for {role_label} ID {person.id if person else 'unknown'}")
            return False

        month_name = salary_obj.month.strftime('%B %Y') if salary_obj.month else 'Current Month'
        subject = f"Your Payslip for {month_name} is Ready - Eagle in Cloud HRMS"
        
        full_name = f"{first_name} {last_name}".strip() or "Team Member"
        basic_val = f"{salary_obj.basic or 0:,}"
        earnings_val = f"{salary_obj.total_earnings or 0:,}"
        deductions_val = f"{salary_obj.total_deductions or 0:,}"
        net_val = f"{salary_obj.net_pay() or 0:,}"

        html_content = f"""<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <style>
    body {{ font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #f8fafc; margin: 0; padding: 0; color: #1e293b; }}
    .email-container {{ max-width: 600px; margin: 30px auto; background: #ffffff; border-radius: 12px; overflow: hidden; border: 1px solid #e2e8f0; box-shadow: 0 4px 6px -1px rgba(0,0,0,0.05); }}
    .header {{ background: linear-gradient(135deg, #07080b 0%, #1e1e24 100%); padding: 28px 24px; text-align: center; border-bottom: 3px solid #FC0101; }}
    .header h1 {{ color: #ffffff; margin: 0; font-size: 22px; font-weight: 700; letter-spacing: 0.5px; }}
    .header p {{ color: #94a3b8; margin: 6px 0 0; font-size: 13px; }}
    .content {{ padding: 32px 28px; }}
    .greeting {{ font-size: 16px; font-weight: 600; margin-bottom: 12px; color: #0f172a; }}
    .message {{ font-size: 14px; line-height: 1.6; color: #475569; margin-bottom: 24px; }}
    .salary-box {{ background: #f8fafc; border: 1px solid #e2e8f0; border-radius: 10px; padding: 20px; margin-bottom: 24px; }}
    .salary-box table {{ width: 100%; border-collapse: collapse; font-size: 14px; }}
    .salary-box td {{ padding: 8px 0; }}
    .salary-box td.label {{ color: #64748b; }}
    .salary-box td.value {{ text-align: right; font-weight: 600; color: #0f172a; }}
    .salary-box tr.net-row td {{ border-top: 1.5px dashed #cbd5e1; padding-top: 12px; font-size: 16px; }}
    .salary-box tr.net-row td.value {{ color: #FC0101; font-size: 18px; font-weight: 800; }}
    .btn-container {{ text-align: center; margin: 30px 0 10px; }}
    .btn {{ background-color: #FC0101; color: #ffffff !important; text-decoration: none; padding: 12px 32px; border-radius: 8px; font-weight: 700; font-size: 14px; display: inline-block; box-shadow: 0 4px 12px rgba(252, 1, 1, 0.25); }}
    .footer {{ background: #f1f5f9; padding: 18px 24px; text-align: center; font-size: 12px; color: #64748b; border-top: 1px solid #e2e8f0; }}
    .footer a {{ color: #FC0101; text-decoration: none; }}
  </style>
</head>
<body>
  <div class="email-container">
    <div class="header">
      <h1>Eagle in Cloud HRMS</h1>
      <p>Monthly Payroll Notification</p>
    </div>
    <div class="content">
      <div class="greeting">Dear {full_name},</div>
      <p class="message">
        We are pleased to inform you that your payslip for <strong>{month_name}</strong> has been generated and is now ready for your review.
      </p>
      
      <div class="salary-box">
        <table>
          <tr>
            <td class="label">Pay Period:</td>
            <td class="value">{month_name}</td>
          </tr>
          <tr>
            <td class="label">Basic Salary:</td>
            <td class="value">₹{basic_val}</td>
          </tr>
          <tr>
            <td class="label">Gross Earnings:</td>
            <td class="value">₹{earnings_val}</td>
          </tr>
          <tr>
            <td class="label">Total Deductions:</td>
            <td class="value">₹{deductions_val}</td>
          </tr>
          <tr class="net-row">
            <td class="label"><strong>Net Salary:</strong></td>
            <td class="value">₹{net_val}</td>
          </tr>
        </table>
      </div>

      <p class="message">
        You can log in to your employee portal at any time to view your detailed breakdown, tax deductions, and download your official PDF payslip.
      </p>

      <div class="btn-container">
        <a href="https://www.eagleinclouds.com" class="btn" target="_blank">Access HRMS Portal</a>
      </div>
    </div>
    <div class="footer">
      This is an automated system email from Eagle in Cloud HRMS. Please do not reply directly.<br>
      © Eagle in Cloud. All rights reserved. (<a href="https://www.eagleinclouds.com">www.eagleinclouds.com</a>)
    </div>
  </div>
</body>
</html>"""

        plain_content = strip_tags(html_content)
        from_email = getattr(settings, 'DEFAULT_FROM_EMAIL', getattr(settings, 'EMAIL_HOST_USER', 'eic.mail.id@gmail.com'))

        msg = EmailMultiAlternatives(subject, plain_content, from_email, [recipient_email])
        msg.attach_alternative(html_content, "text/html")
        msg.send(fail_silently=False)
        logger.info(f"Payslip email sent to {recipient_email} for {month_name}")
        return True
    except Exception as e:
        logger.error(f"Failed to send payslip email: {e}")
        return False


def generate_monthly_payslips(target_month=None, company_id=None, send_email=True, force=False):
    """
    Main function to auto-generate payslips for all active Employees & Managers.
    - target_month: date object (e.g. date(2026, 9, 1)) or None (defaults to previous month)
    - company_id: specific company ID or None for all
    - send_email: boolean, send email notifications
    - force: boolean, overwrite/regenerate if already exists
    """
    from employee.models import Employee
    from managers.models import Manager
    from payroll.models import Salary as EmployeeSalary
    from managerpayroll.models import Salary as ManagerSalary
    from account.models import Company

    if target_month is None:
        target_month = get_default_target_month()
    elif isinstance(target_month, str):
        try:
            # Parse 'YYYY-MM' or 'YYYY-MM-DD'
            if len(target_month) == 7:
                target_month = datetime.strptime(target_month, '%Y-%m').date()
            else:
                target_month = datetime.strptime(target_month, '%Y-%m-%d').date()
            target_month = date(target_month.year, target_month.month, 1)
        except Exception:
            target_month = get_default_target_month()

    results = {
        "status": "success",
        "target_month": target_month.strftime('%Y-%m-%d'),
        "target_month_name": target_month.strftime('%B %Y'),
        "employees_created": 0,
        "employees_skipped": 0,
        "managers_created": 0,
        "managers_skipped": 0,
        "emails_sent": 0,
        "details": []
    }

    # 1. Process Employees
    emp_qs = Employee.objects.filter(employee_status='Active')
    if company_id:
        emp_qs = emp_qs.filter(user__company_id=company_id)

    for emp in emp_qs:
        existing = EmployeeSalary.objects.filter(employee=emp, month=target_month).first()
        if existing and not force:
            results["employees_skipped"] += 1
            continue

        # Fetch most recent previous salary template
        last_salary = EmployeeSalary.objects.filter(employee=emp).order_by('-month', '-id').first()
        
        if existing and force:
            salary_record = existing
        else:
            salary_record = EmployeeSalary(employee=emp, month=target_month)

        if last_salary:
            salary_record.basic = last_salary.basic or 0
            salary_record.da_percent = last_salary.da_percent or 0
            salary_record.hra_percent = last_salary.hra_percent or 0
            salary_record.conveyance = last_salary.conveyance or 0
            salary_record.bonuses = 0
            salary_record.allowance = last_salary.allowance or 0
            salary_record.medical_allowance = last_salary.medical_allowance or 0
            salary_record.tds = last_salary.tds or 0
            salary_record.esi = last_salary.esi or 0
            salary_record.providence_fund = last_salary.providence_fund or 0
            salary_record.leave = 0
            salary_record.tax = last_salary.tax or 0
            salary_record.labour_welfare = last_salary.labour_welfare or 0
            salary_record.loan_repayment = 0
            salary_record.others = last_salary.others or 0
        else:
            # Fallback to employee_salary field
            try:
                base_sal = int(float(emp.employee_salary or 0))
            except Exception:
                base_sal = 25000
            salary_record.basic = base_sal
            salary_record.da_percent = 0
            salary_record.hra_percent = 0
            salary_record.conveyance = 0
            salary_record.bonuses = 0
            salary_record.allowance = 0
            salary_record.medical_allowance = 0
            salary_record.tds = 0
            salary_record.esi = 0
            salary_record.providence_fund = 0
            salary_record.leave = 0
            salary_record.tax = 0
            salary_record.labour_welfare = 0
            salary_record.loan_repayment = 0
            salary_record.others = 0

        salary_record.save()
        results["employees_created"] += 1
        results["details"].append(f"Created Employee Salary: {emp.employee_first_name} {emp.employee_last_name} ({target_month.strftime('%b %Y')})")

        if send_email:
            sent = send_payslip_email(salary_record, is_manager=False)
            if sent:
                results["emails_sent"] += 1

    # 2. Process Managers
    mgr_qs = Manager.objects.filter(manager_status='Active')
    if company_id:
        mgr_qs = mgr_qs.filter(user__company_id=company_id)

    for mgr in mgr_qs:
        existing = ManagerSalary.objects.filter(manager=mgr, month=target_month).first()
        if existing and not force:
            results["managers_skipped"] += 1
            continue

        last_salary = ManagerSalary.objects.filter(manager=mgr).order_by('-month', '-id').first()

        if existing and force:
            salary_record = existing
        else:
            salary_record = ManagerSalary(manager=mgr, month=target_month)

        if last_salary:
            salary_record.basic = last_salary.basic or 0
            salary_record.da_percent = last_salary.da_percent or 0
            salary_record.hra_percent = last_salary.hra_percent or 0
            salary_record.conveyance = last_salary.conveyance or 0
            salary_record.bonuses = 0
            salary_record.allowance = last_salary.allowance or 0
            salary_record.medical_allowance = last_salary.medical_allowance or 0
            salary_record.tds = last_salary.tds or 0
            salary_record.esi = last_salary.esi or 0
            salary_record.providence_fund = last_salary.providence_fund or 0
            salary_record.leave = 0
            salary_record.tax = last_salary.tax or 0
            salary_record.labour_welfare = last_salary.labour_welfare or 0
            salary_record.loan_repayment = 0
            salary_record.others = last_salary.others or 0
        else:
            try:
                base_sal = int(float(mgr.manager_salary or 0))
            except Exception:
                base_sal = 35000
            salary_record.basic = base_sal
            salary_record.da_percent = 0
            salary_record.hra_percent = 0
            salary_record.conveyance = 0
            salary_record.bonuses = 0
            salary_record.allowance = 0
            salary_record.medical_allowance = 0
            salary_record.tds = 0
            salary_record.esi = 0
            salary_record.providence_fund = 0
            salary_record.leave = 0
            salary_record.tax = 0
            salary_record.labour_welfare = 0
            salary_record.loan_repayment = 0
            salary_record.others = 0

        salary_record.save()
        results["managers_created"] += 1
        results["details"].append(f"Created Manager Salary: {mgr.manager_first_name} {mgr.manager_last_name} ({target_month.strftime('%b %Y')})")

        if send_email:
            sent = send_payslip_email(salary_record, is_manager=True)
            if sent:
                results["emails_sent"] += 1

    return results


def run_scheduled_payslip_check():
    """
    Called by background schedulers / daemons.
    If today is the 10th (or later in the month and previous month's payslips have not been run),
    automatically runs payslip generation.
    """
    today = date.today()
    target_month = get_default_target_month(today)
    
    # We execute auto-generation when today's day is 10 or later
    if today.day >= 10:
        from payroll.models import Salary as EmployeeSalary
        # Check if we already have payslips generated for this target month
        has_slips = EmployeeSalary.objects.filter(month=target_month).exists()
        if not has_slips:
            logger.info(f"Auto-payslip trigger active for {target_month.strftime('%B %Y')} on day {today.day}")
            res = generate_monthly_payslips(target_month=target_month, send_email=True)
            logger.info(f"Auto-payslip completed: {res}")
            return res
    return None
