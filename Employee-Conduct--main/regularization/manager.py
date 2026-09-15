from django.db import models
import datetime

from django.shortcuts import redirect

from account.models import CompanyStaff


class  regularizationManager(models.Manager):

    def get_queryset(self):

        return super().get_queryset()

    def all_pending_regularization(self):
        return super().get_queryset().filter(status='pending').order_by('-created')  # applying FIFO

    def all_cancel_regularization(self):
        return super().get_queryset().filter(status='cancelled').order_by('-created')

    def all_rejected_regularization(self):
        return super().get_queryset().filter(status='rejected').order_by('-created')

    def all_approved_regularization(self):
        return super().get_queryset().filter(status='approved')

    def current_year_regularization(self):
        return super().get_queryset().filter(startdate__year=datetime.date.today().year)
