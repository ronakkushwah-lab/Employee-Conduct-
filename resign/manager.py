from django.db import models
import datetime


class ResignManager(models.Manager):
    def get_queryset(self):

        return super().get_queryset()

    def all_pending_resign(self):
        return super().get_queryset().filter(status='pending').order_by('-created')  # applying FIFO

    def all_cancel_resign(self):
        return super().get_queryset().filter(status='cancelled').order_by('-created')

    def all_rejected_resign(self):
        return super().get_queryset().filter(status='rejected').order_by('-created')

    def all_approved_resign(self):
        return super().get_queryset().filter(status='approved')

    def current_year_resign(self):
        return super().get_queryset().filter(startdate__year=datetime.date.today().year)
