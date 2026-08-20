from django.db import models
from .manager import ResignManager
from django.utils.translation import gettext as _
from django.contrib.auth.models import User
from django.utils import timezone
from datetime import datetime
from employee.models import Employee
from managers.models import Manager
from django.contrib.auth import get_user_model

# User = get_user_model()


# Create your models here.


class ManagerResign(models.Model):
    user = models.ForeignKey(Manager, on_delete=models.CASCADE, null=True, blank=True)
    assigned_too = models.ForeignKey(
        Manager,
        on_delete=models.CASCADE,
        null=True,
        blank=True,
        related_name='manager_resignations_to_approve',
        verbose_name=_('Reporting to / Approving Manager'),
    )
    startdate = models.DateField(verbose_name=_('Start Date'), help_text='resign start date is on ..', null=True,
                                 blank=False)
    reason = models.CharField(verbose_name=_('Reason for Resignation'), max_length=255,
                              help_text='add additional information for resign', null=True, blank=True)

    status = models.CharField(max_length=12, default='pending')  # pending,approved,rejected,cancelled
    is_approved = models.BooleanField(default=False)  # hide

    updated = models.DateTimeField(auto_now=True, auto_now_add=False)
    created = models.DateTimeField(auto_now=False, auto_now_add=True)

    objects = ResignManager()

    class Meta:
        verbose_name = _('Resign')
        verbose_name_plural = _('Resigns')
        ordering = ['-created']  # recent objects


    @property
    def resign_approved(self):
        return self.is_approved == True

    @property
    def approve_resign(self):
        if not self.is_approved:
            self.is_approved = True
            self.status = 'approved'
            self.save()

    @property
    def unapprove_resign(self):
        if self.is_approved:
            self.is_approved = False
            self.status = 'pending'
            self.save()

    @property
    def resign_cancel(self):
        if self.is_approved or not self.is_approved:
            self.is_approved = False
            self.status = 'cancelled'
            self.save()

    @property
    def reject_resign(self):
        if self.is_approved or not self.is_approved:
            self.is_approved = False
            self.status = 'rejected'
            self.save()

    @property
    def is_rejected(self):
        return self.status == 'rejected'

    def to_json(self):
        resign_details_dict = {
            'id': self.id,
            'user': self.user.manager_email,
            'startdate': self.startdate,
            'reason': self.reason,
            'status': self.status,
            }
        return resign_details_dict

