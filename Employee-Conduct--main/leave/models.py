from django.db import models
from django.urls import reverse

from employee.models import Employee
from .manager import LeaveManager
from django.utils.translation import gettext as _
from django.contrib.auth.models import User
from django.utils import timezone
from datetime import datetime
from django.contrib.auth import get_user_model

User = get_user_model()

# Create your models here.
SICK = 'sick'
CASUAL = 'casual'
EMERGENCY = 'emergency'

LEAVE_TYPE = (
    (SICK, 'Sick Leave'),
    (CASUAL, 'Casual Leave'),
    (EMERGENCY, 'Emergency Leave'),
)
DAYS = 30


class Leave(models.Model):
    user = models.ForeignKey(Employee, on_delete=models.CASCADE,null=True,blank=True)
    manager = models.ForeignKey('managers.Manager', on_delete=models.SET_NULL, null=True, blank=True, verbose_name=_('Manager'))
    startdate = models.DateField(verbose_name=_('Start Date'), help_text='leave start date is on ..', null=True,
                                 blank=False)
    enddate = models.DateField(verbose_name=_('End Date'), help_text='coming back on ...', null=True, blank=False)
    leavetype = models.CharField(verbose_name=_('Leave Type'),choices=LEAVE_TYPE, max_length=25, default=SICK, null=True, blank=False)
    reason = models.CharField(verbose_name=_('Reason for Leave'), max_length=255,
                              help_text='add additional information for leave', null=True, blank=True)
    description = models.TextField(verbose_name=_('Description'), 
                                   help_text='detailed description of the leave', null=True, blank=True)

    status = models.CharField(max_length=30, default='pending_manager')  # pending_manager,pending_hr,approved,rejected,cancelled
    is_approved = models.BooleanField(default=False)  # final HR approval
    manager_approved = models.BooleanField(default=False)
    manager_approved_at = models.DateTimeField(null=True, blank=True)

    updated = models.DateTimeField(auto_now=True, auto_now_add=False)
    created = models.DateTimeField(auto_now=False, auto_now_add=True)

    objects = LeaveManager()

    class Meta:
        verbose_name = _('Leave')
        verbose_name_plural = _('Leaves')
        ordering = ['-created']  # recent objects

    def __str__(self):
        return ('{0} - {1}'.format(self.leavetype, self.user))

    @property
    def pretty_leave(self):

        leave = self.leavetype
        user = self.user
        employee = user.employee_set.first().get_full_name
        return ('{0} - {1}'.format(employee, leave))

    @property
    def leave_days(self):
        if not self.startdate or not self.enddate:
            return 0
        if self.startdate > self.enddate:
            return 0
        dates = (self.enddate - self.startdate)
        return dates.days + 1

    @property
    def leave_approved(self):
        return self.is_approved == True

    @property
    def approve_by_manager(self):
        self.manager_approved = True
        self.manager_approved_at = timezone.now()
        self.status = 'pending_hr'
        self.save()

    @property
    def approve_leave(self):
        if not self.is_approved:
            self.is_approved = True
            self.status = 'approved'
            self.save()

    # @property
    # def leave_count(self):
    #     leaves = User.objects.filter(id=self.id,leave__status='approve_leave').aggregate(leave_count=Count('leave'))
    #     return leaves['leave_count']

    # @property
    # def leave_count(self):
    #     leaves = Leave.objects.filter(id=self.id)
    #     count = 0
    #     for leave in leaves:
    #         if leave.status == "approve_leave":
    #             count += 1
    #     return count

    @property
    def unapprove_leave(self):
        if self.is_approved:
            self.is_approved = False
            self.status = 'pending_manager'
            self.save()

    @property
    def leaves_cancel(self):
        if self.is_approved or not self.is_approved:
            self.is_approved = False
            self.status = 'cancelled'
            self.save()

    @property
    def reject_leave(self):
        if self.is_approved or not self.is_approved:
            self.is_approved = False
            self.status = 'rejected'
            self.save()

    @property
    def is_rejected(self):
        return self.status == 'rejected'

    def get_absolute_url(self):
        return reverse("balance-leave")

    def to_json(self):
        mgr_name = f"{self.manager.manager_first_name} {self.manager.manager_last_name}".strip() if self.manager else "No Manager Assigned"
        leave_details_dict = {
            'id': self.id,
            'startdate': self.startdate,
            'enddate': self.enddate,
            'reason': self.reason,
            'status': self.status,
            'leavetype': self.leavetype,
            'leave_days': self.leave_days,
            'created': self.created,
            'manager_approved': self.manager_approved,
            'manager_name': mgr_name,
            'has_manager': bool(self.manager),
            'is_manager_leave': False,
        }
        return leave_details_dict


class BalanceLeaves(models.Model):
    user = models.ForeignKey(Employee, on_delete=models.CASCADE, null=True, blank=True)
    leaves = models.ForeignKey(Leave, on_delete=models.CASCADE, null=True, blank=True)
    balancedays = models.PositiveIntegerField(verbose_name=_('Leave days per year counter'), default=10, null=True,
                                              blank=True)
    created = models.DateTimeField(auto_now=False, auto_now_add=True, null=True)

    def __str__(self):
        return ('{0} - {1}'.format(self.balancedays, self.user))

    @property
    def used_days(self):
        if not self.user:
            return 0
        active_leaves = Leave.objects.filter(
            user=self.user
        ).exclude(status__in=['rejected', 'cancelled', 'canceled'])
        return sum(l.leave_days for l in active_leaves if l.leave_days)

    @property
    def remaining_days(self):
        total = self.balancedays or 0
        return max(0, total - self.used_days)

    @classmethod
    def get_balance_summary(cls, employee):
        if not employee:
            return {
                'total_allocated': 0,
                'used_days': 0,
                'approved_days': 0,
                'pending_days': 0,
                'remaining_balance': 0,
            }

        balances = cls.objects.filter(user=employee)
        if balances.exists():
            total_allocated = sum(b.balancedays or 0 for b in balances)
        else:
            total_allocated = 10

        all_leaves = Leave.objects.filter(user=employee)
        active_leaves = all_leaves.exclude(status__in=['rejected', 'cancelled', 'canceled'])

        used_days = sum(l.leave_days for l in active_leaves if l.leave_days)
        approved_days = sum(l.leave_days for l in all_leaves.filter(is_approved=True) if l.leave_days)
        pending_days = sum(l.leave_days for l in active_leaves.filter(is_approved=False) if l.leave_days)
        remaining_balance = max(0, total_allocated - used_days)

        return {
            'total_allocated': total_allocated,
            'used_days': used_days,
            'approved_days': approved_days,
            'pending_days': pending_days,
            'remaining_balance': remaining_balance,
        }
