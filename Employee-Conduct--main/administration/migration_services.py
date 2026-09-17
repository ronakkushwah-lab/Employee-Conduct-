import logging
from datetime import datetime, timedelta
from django.utils import timezone as tz
from django.db import transaction

from employee.models import Attendance, Employee
from managers.models import Manager, ManagerAttendance
from leave.models import Leave, BalanceLeaves as EmpBalanceLeave
from manager_leave.models import ManagerLeave, BalanceLeave as MgrBalanceLeave
from regularization.models import Regularization
from manageregularization.models import MRegularization
from payroll.models import Salary as EmpSalary
from managerpayroll.models import Salary as MgrSalary
from biometric.models import BiometricEventLog

logger = logging.getLogger(__name__)


def reprocess_biometric_logs_for_identity(biometric_user_id, manager=None, employee=None):
    """
    Reprocess all raw/unmatched BiometricEventLog records for a given biometric_user_id
    and populate ManagerAttendance (if manager) or Attendance (if employee).
    """
    bio_id = str(biometric_user_id).strip()
    if not bio_id:
        return 0

    logs = BiometricEventLog.objects.filter(biometric_user_id=bio_id).order_by('punch_time')
    if not logs.exists():
        return 0

    # Group punches by local date
    daily_punches = {}
    for log in logs:
        local_time = tz.localtime(log.punch_time) if tz.is_aware(log.punch_time) else log.punch_time
        d = local_time.date()
        daily_punches.setdefault(d, []).append((log, local_time, log.punch_time))

    synced_days = 0
    for day_date, punch_list in daily_punches.items():
        if not punch_list:
            continue

        first_log, first_local, first_aware = punch_list[0]
        last_log, last_local, last_aware = punch_list[-1]

        check_in = first_aware
        check_out = None
        # If last punch is at least 5 minutes after first punch, treat as check_out
        if len(punch_list) > 1 and (last_aware - first_aware).total_seconds() >= 300:
            check_out = last_aware

        if manager:
            # Check if ManagerAttendance already exists for this day
            mgr_att = ManagerAttendance.objects.filter(
                manager=manager,
                check_in__date=day_date
            ).first()

            if mgr_att:
                if not mgr_att.check_out and check_out:
                    mgr_att.check_out = check_out
                    mgr_att.save(update_fields=['check_out'])
            else:
                mgr_att = ManagerAttendance.objects.create(
                    manager=manager,
                    check_in=check_in,
                    check_out=check_out
                )
            synced_days += 1

            # Update BiometricEventLog records
            for log_item, _, _ in punch_list:
                log_item.status = BiometricEventLog.STATUS_APPLIED
                log_item.manager = manager
                log_item.manager_attendance = mgr_att
                log_item.message = f"Applied to Manager Attendance (ID: {mgr_att.id})"
                log_item.save(update_fields=['status', 'manager', 'manager_attendance', 'message'])

        elif employee:
            emp_att = Attendance.objects.filter(
                employee=employee,
                check_in__date=day_date
            ).first()

            if emp_att:
                if not emp_att.check_out and check_out:
                    emp_att.check_out = check_out
                    emp_att.save(update_fields=['check_out'])
            else:
                emp_att = Attendance.objects.create(
                    employee=employee,
                    check_in=check_in,
                    check_out=check_out
                )
            synced_days += 1

            # Update BiometricEventLog records
            for log_item, _, _ in punch_list:
                log_item.status = BiometricEventLog.STATUS_APPLIED
                log_item.employee = employee
                log_item.attendance = emp_att
                log_item.message = f"Applied to Employee Attendance (ID: {emp_att.id})"
                log_item.save(update_fields=['status', 'employee', 'attendance', 'message'])

    return synced_days


def migrate_employee_history_to_manager(employee=None, manager=None, user=None):
    """
    Seamlessly migrates/copies 100% of an Employee's historical data into their Manager profile:
    - Attendance records -> ManagerAttendance
    - Raw & unmatched BiometricEventLogs -> Re-linked and reprocessed
    - Leaves -> ManagerLeave
    - Leave balances -> manager_leave.BalanceLeave
    - Regularization requests -> MRegularization
    - Payroll / Salary slips -> managerpayroll.Salary
    - Profile images, base64 avatars, contact details
    """
    if not manager and user:
        manager = Manager.objects.filter(user=user).first()
    if not employee and user:
        employee = Employee.objects.filter(user=user).first()
    if not employee and manager and manager.user:
        employee = Employee.objects.filter(employee_email=manager.user.email).first()

    if not manager:
        logger.warning("migrate_employee_history_to_manager: No manager profile provided or found.")
        return False

    with transaction.atomic():
        # 1. Profile fields sync
        if employee:
            if not manager.biometric_id and employee.biometric_id:
                manager.biometric_id = employee.biometric_id
            if not manager.manager_image and employee.employee_image:
                manager.manager_image = employee.employee_image
            if not manager.avatar_base64 and getattr(employee, 'avatar_base64', None):
                manager.avatar_base64 = employee.avatar_base64
            if not manager.manager_joining_date and employee.employee_joining_date:
                manager.manager_joining_date = employee.employee_joining_date
            if not manager.manager_phone and employee.employee_phone:
                manager.manager_phone = employee.employee_phone
            if not manager.manager_department and employee.employee_department:
                manager.manager_department = employee.employee_department
            manager.save()

        # 2. Attendance Migration (employee Attendance -> ManagerAttendance)
        if employee:
            emp_attendances = Attendance.objects.filter(employee=employee)
        elif manager.user:
            emp_attendances = Attendance.objects.filter(employee__employee_email=manager.user.email)
        else:
            emp_attendances = Attendance.objects.none()

        for emp_att in emp_attendances:
            if not emp_att.check_in:
                continue
            att_date = emp_att.check_in.date()
            mgr_att, created = ManagerAttendance.objects.get_or_create(
                manager=manager,
                check_in__date=att_date,
                defaults={
                    'check_in': emp_att.check_in,
                    'check_out': emp_att.check_out
                }
            )
            if not created and not mgr_att.check_out and emp_att.check_out:
                mgr_att.check_out = emp_att.check_out
                mgr_att.save(update_fields=['check_out'])

        # 3. Biometric Raw Logs Migration & Reprocessing
        bio_id = manager.biometric_id or (employee.biometric_id if employee else None)
        if bio_id:
            reprocess_biometric_logs_for_identity(bio_id, manager=manager)

        # Also re-link any event logs directly pointing to employee
        if employee:
            BiometricEventLog.objects.filter(employee=employee).update(
                manager=manager,
                status=BiometricEventLog.STATUS_APPLIED
            )

        # 4. Leaves Migration (Leave -> ManagerLeave)
        if employee:
            emp_leaves = Leave.objects.filter(user=employee)
            for el in emp_leaves:
                MgrLeave_exists = ManagerLeave.objects.filter(
                    user=manager,
                    startdate=el.startdate,
                    enddate=el.enddate,
                    leavetype=el.leavetype
                ).exists()
                if not MgrLeave_exists:
                    ManagerLeave.objects.create(
                        user=manager,
                        startdate=el.startdate,
                        enddate=el.enddate,
                        leavetype=el.leavetype,
                        reason=el.reason or '',
                        description=getattr(el, 'description', '') or '',
                        status=el.status,
                        is_approved=el.is_approved,
                        created=el.created
                    )

            # 5. Leave Balance Migration
            emp_balances = EmpBalanceLeave.objects.filter(user=employee)
            if emp_balances.exists():
                for eb in emp_balances:
                    MgrBalanceLeave.objects.get_or_create(
                        user=manager,
                        balancedays=eb.balancedays,
                        defaults={'created': eb.created}
                    )

        # 6. Regularization Migration (Regularization -> MRegularization)
        if employee:
            emp_regs = Regularization.objects.filter(user=employee)
            for er in emp_regs:
                reg_exists = MRegularization.objects.filter(
                    user=manager,
                    check_in=er.check_in,
                    check_out=er.check_out
                ).exists()
                if not reg_exists:
                    MRegularization.objects.create(
                        user=manager,
                        check_in=er.check_in,
                        check_out=er.check_out,
                        reason=er.reason or '',
                        status=er.status,
                        is_approved=er.is_approved,
                        created=er.created
                    )

        # 7. Salary / Payroll Migration (payroll.Salary -> managerpayroll.Salary)
        if employee:
            emp_salaries = EmpSalary.objects.filter(employee=employee)
            for es in emp_salaries:
                sal_exists = MgrSalary.objects.filter(
                    manager=manager,
                    month=es.month
                ).exists()
                if not sal_exists:
                    MgrSalary.objects.create(
                        manager=manager,
                        month=es.month,
                        basic=es.basic,
                        da_percent=es.da_percent,
                        hra_percent=es.hra_percent,
                        conveyance=es.conveyance,
                        bonuses=es.bonuses,
                        allowance=es.allowance,
                        medical_allowance=es.medical_allowance,
                        tds=es.tds,
                        esi=es.esi,
                        providence_fund=es.providence_fund,
                        leave=es.leave,
                        tax=es.tax,
                        labour_welfare=es.labour_welfare,
                        loan_repayment=es.loan_repayment,
                        others=es.others
                    )

    return True
