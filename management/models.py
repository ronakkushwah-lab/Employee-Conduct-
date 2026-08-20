from django.db import models

from account.models import Company
from employee.models import Employee

class Invoice(models.Model):
    company = models.ForeignKey(Company, on_delete=models.CASCADE, default=True)
    client = models.CharField(max_length=100,blank=False)
    client_email = models.EmailField(null=True, blank=False)
    billing_address = models.TextField(null=True, blank=False)
    date = models.DateField()
    due_date = models.DateField(null=True, blank=True)
    project = models.CharField(max_length=100,blank=False)
    total_amount = models.DecimalField(max_digits=9, decimal_places=2, blank=True, null=True)
    status = models.BooleanField(default=False)

    def __str__(self):
        return str(self.client)

    def get_status(self):
        return self.status


class LineItem(models.Model):
    client = models.ForeignKey(Invoice, on_delete=models.CASCADE)
    service = models.TextField()
    description = models.TextField()
    quantity = models.IntegerField()
    rate = models.DecimalField(max_digits=9, decimal_places=2)
    amount = models.DecimalField(max_digits=9, decimal_places=2)

    def __str__(self):
        return str(self.client)
