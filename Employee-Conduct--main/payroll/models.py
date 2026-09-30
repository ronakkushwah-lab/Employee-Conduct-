from django.db import models
from django.urls import reverse

from employee.models import Employee
from core.encryption import EncryptedIntegerField


class Salary(models.Model):
    employee = models.ForeignKey(Employee,
                              on_delete=models.CASCADE,null=True,default=True,blank=True)
    month = models.DateField(auto_now=False, auto_now_add=False)
    basic = EncryptedIntegerField(default=0)
    da_percent = EncryptedIntegerField(default=0, blank=True, null=True)
    hra_percent = EncryptedIntegerField(
        "House rent Allowance", default=0, blank=True, null=True)
    conveyance = EncryptedIntegerField(default=0, blank=True, null=True)
    bonuses = EncryptedIntegerField(default=0, blank=True, null=True)
    allowance = EncryptedIntegerField(default=0, blank=True, null=True)
    medical_allowance = EncryptedIntegerField(
        default=0, blank=True, null=True)
    tds = EncryptedIntegerField(
        "Tax Deducted at Source (T.D.S.)", default=0, blank=True, null=True)
    esi = EncryptedIntegerField(default=0, blank=True, null=True)
    providence_fund = EncryptedIntegerField(
        "Provident Fund", default=0, blank=True, null=True)
    leave = EncryptedIntegerField(default=0, blank=True, null=True)
    tax = EncryptedIntegerField(default=0, blank=True, null=True)
    labour_welfare = EncryptedIntegerField(
        default=0, blank=True, null=True)
    loan_repayment = EncryptedIntegerField(
        default=0, blank=True, null=True)
    others = EncryptedIntegerField(default=0, blank=True, null=True)

    @property
    def total_earnings(self):
        return (self.basic or 0) + (self.da_percent or 0) + (self.hra_percent or 0) + (self.conveyance or 0) + (self.bonuses or 0) + (self.allowance or 0) + (self.medical_allowance or 0)

    @property
    def total_deductions(self):
        return (self.tds or 0) + (self.esi or 0) + (self.providence_fund or 0) + (self.leave or 0) + (self.tax or 0) + (self.others or 0)

    def net_pay(self):
        return self.total_earnings - self.total_deductions

    class Meta:
        """Meta definition for Payroll."""
        verbose_name = 'Salary'
        verbose_name_plural = 'Salaries'

    def get_absolute_url(self,company_id, company_staff_id,):
        return reverse('salary-detail',{'company_id':company_id,'company_staff_id':company_staff_id}, kwargs={'pk': self.pk})

    def __str__(self):
        return f"{self.employee} salary"
