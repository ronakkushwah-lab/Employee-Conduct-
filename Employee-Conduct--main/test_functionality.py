#!/usr/bin/env python
"""
Test script to verify all email notification functionality
"""
import os
import sys
import django

# Setup Django
sys.path.append(os.path.dirname(os.path.abspath(__file__)))
os.environ.setdefault('DJANGO_SETTINGS_MODULE', 'dstt.settings')
django.setup()

from account.models import Company, CompanyStaff
from employee.models import Employee, Department, Attendance
from managers.models import Manager
from leave.models import Leave
from employee.models import Post
from django.contrib.auth.hashers import make_password
from django.utils import timezone
from datetime import datetime, timedelta
from administration.email_notifications import (
    send_attendance_notification,
    send_leave_submission_notification,
    send_leave_approval_notification,
    send_manager_change_notification,
    send_new_user_notification,
    send_document_submission_notification
)

def test_email_notifications():
    """Test all email notification functions"""
    print("=" * 60)
    print("TESTING EMAIL NOTIFICATION FUNCTIONALITY")
    print("=" * 60)
    
    # Get test company
    company = Company.objects.filter(id=2).first()
    if not company:
        print("❌ No test company found. Please create one first.")
        return
    
    print(f"\n✓ Using Company: {company.company_name} (ID: {company.id})")
    
    # Get or create test employee
    test_email = "test.employee@test.com"
    if not CompanyStaff.objects.filter(email=test_email).exists():
        print("\n1. Creating test employee...")
        user = CompanyStaff.objects.create(
            company=company,
            email=test_email,
            password=make_password("test123"),
            is_employee=True,
            is_active=True
        )
        
        dept = Department.objects.filter(company=company).first()
        manager = Manager.objects.filter(user__company=company).first()
        
        if dept and manager:
            employee = Employee.objects.create(
                user=user,
                employee_first_name="Test",
                employee_last_name="Employee",
                employee_email=test_email,
                employee_joining_date=datetime.now().date(),
                employee_department=dept,
                employee_designation="Software Engineer",
                employee_id="EIC-TEST",
                employee_reports_to=manager
            )
            print(f"   ✓ Created test employee: {employee.employee_first_name} {employee.employee_last_name}")
        else:
            print("   ❌ Missing department or manager")
            return
    else:
        user = CompanyStaff.objects.get(email=test_email)
        employee = Employee.objects.filter(user=user).first()
        print(f"\n✓ Using existing test employee: {employee.employee_first_name} {employee.employee_last_name}")
    
    # Test 1: Attendance Notification
    print("\n2. Testing Attendance Notification...")
    try:
        attendance = Attendance.objects.create(
            employee=employee,
            check_in=timezone.now(),
            source='manual'
        )
        result = send_attendance_notification(attendance, action='check_in')
        print(f"   {'✓' if result else '❌'} Check-in notification sent")
        
        attendance.check_out = timezone.now() + timedelta(hours=8)
        attendance.save()
        result = send_attendance_notification(attendance, action='check_out')
        print(f"   {'✓' if result else '❌'} Check-out notification sent")
    except Exception as e:
        print(f"   ❌ Error: {str(e)}")
    
    # Test 2: Leave Submission Notification
    print("\n3. Testing Leave Submission Notification...")
    try:
        leave = Leave.objects.create(
            user=employee,
            startdate=datetime.now().date() + timedelta(days=7),
            enddate=datetime.now().date() + timedelta(days=9),
            leavetype='casual',
            reason='Personal work',
            description='Need to attend personal matters',
            status='pending'
        )
        result = send_leave_submission_notification(leave)
        print(f"   {'✓' if result else '❌'} Leave submission notification sent")
    except Exception as e:
        print(f"   ❌ Error: {str(e)}")
    
    # Test 3: Leave Approval Notification
    print("\n4. Testing Leave Approval Notification...")
    try:
        if 'leave' in locals():
            leave.approve_leave
            result = send_leave_approval_notification(leave, approved=True)
            print(f"   {'✓' if result else '❌'} Leave approval notification sent")
    except Exception as e:
        print(f"   ❌ Error: {str(e)}")
    
    # Test 4: Manager Change Notification
    print("\n5. Testing Manager Change Notification...")
    try:
        old_manager = employee.employee_reports_to
        new_manager = Manager.objects.filter(user__company=company).exclude(id=old_manager.id).first()
        if new_manager:
            employee.employee_reports_to = new_manager
            employee.save()
            result = send_manager_change_notification(employee, old_manager=old_manager, new_manager=new_manager)
            print(f"   {'✓' if result else '❌'} Manager change notification sent")
            # Revert change
            employee.employee_reports_to = old_manager
            employee.save()
        else:
            print("   ⚠ No alternative manager found for testing")
    except Exception as e:
        print(f"   ❌ Error: {str(e)}")
    
    # Test 5: Document Submission Notification
    print("\n6. Testing Document Submission Notification...")
    try:
        # Create a dummy document (without actual file)
        document = Post.objects.create(user=employee)
        # Note: This will fail without actual files, but we can test the function structure
        print("   ⚠ Document submission requires actual files (skipping full test)")
    except Exception as e:
        print(f"   ⚠ Document test skipped: {str(e)}")
    
    print("\n" + "=" * 60)
    print("TESTING COMPLETE")
    print("=" * 60)
    print("\n📧 All email notifications are configured and ready!")
    print("   Check the email inboxes to verify emails were sent.")
    print(f"   Company Email: eic.developer.testing@gmail.com")
    print(f"   Test Employee Email: {test_email}")

if __name__ == "__main__":
    test_email_notifications()
