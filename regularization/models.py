from django.db import models
from django.urls import reverse

from employee \
    .models import Attendance, Employee
from managers.models import Manager
from .manager import regularizationManager
from django.utils.translation import gettext as _
from django.contrib.auth.models import User
from django.utils import timezone
from datetime import datetime
from django.contrib.auth import get_user_model


from django.utils import timezone
from datetime import timedelta, datetime
from django.core.exceptions import ValidationError

from django.urls import reverse


# Create your models here.
def validate_start_time(value):
    """
    Validate that a Entry should have a starting date & time in present
    or Future (with 5 Minute negotation)
    """
    if value > timezone.now():
        raise ValidationError(
            " Date should not be  in Present or Future")


def validate_end_time(value):
    """
    Validate that a Entry should have a ending less than 6 months
    """
    print("validating end time")
    if value > timezone.now() + timedelta(days=31 * 6):
        raise ValidationError(
            "Ending Time should be less than 6 months")



class Regularization(models.Model):
    user = models.ForeignKey(Employee, on_delete=models.CASCADE, default=None)
    check_in = models.DateTimeField(validators=[validate_start_time],null=True,blank=False)
    check_out = models.DateTimeField(validators=[validate_end_time], null=True,blank=False)
    reason = models.CharField(verbose_name=_('Reason for Regularization'), max_length=255,
                              help_text='add additional information for Regularization',null=True)

    status = models.CharField(max_length=12, default='pending')  # pending,approved,rejected,cancelled
    is_approved = models.BooleanField(default=False)  # hide
    updated = models.DateTimeField(auto_now=True, auto_now_add=False)
    created = models.DateTimeField(auto_now=False, auto_now_add=True)
    r_assigned_to = models.ForeignKey(
        Manager,
        null=True,
        blank=True,
        related_name="r_assigned_to",
        on_delete=models.CASCADE,
    )
    objects = regularizationManager()

    class Meta:
        verbose_name = _('Regularization')
        verbose_name_plural = _('Regularization')
        ordering = ['-created']  # recent objects

    def __str__(self):
        return self.status

    @property
    def  regularization_approved(self):
        return self.is_approved == True

    @property
    def approve_regularization(self):
        if not self.is_approved:
            self.is_approved = True
            self.status = 'approved'
            self.save()

    @property
    def unapprove_regularization(self):
        if self.is_approved:
            self.is_approved = False
            self.status = 'pending'
            self.save()

    @property
    def  regularization_cancel(self):
        if self.is_approved or not self.is_approved:
            self.is_approved = False
            self.status = 'cancelled'
            self.save()

    @property
    def reject_regularization(self):
        if self.is_approved or not self.is_approved:
            self.is_approved = False
            self.status = 'rejected'
            self.save()

    @property
    def is_rejected(self):
        return self.status == 'rejected'

    def to_json(self):
        regularization_details_dict = {
            'id': self.id,
            'user': self.user.employee_email,
            'check_in': self.check_in,
            'check_out': self.check_out,
            'reason': self.reason,
            'status': self.status,
            }
        return regularization_details_dict

