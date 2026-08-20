from django.db import models
from django.urls import reverse

from employee.models import Employee


class Salary(models.Model):
    employee = models.ForeignKey(Employee,
                              on_delete=models.CASCADE,null=True,default=True,blank=True)
    month = models.DateField(auto_now=False, auto_now_add=False)
    basic = models.PositiveIntegerField(default=0)
    da_percent = models.PositiveIntegerField(default=0, blank=True, null=True)
    hra_percent = models.PositiveIntegerField(
        "House rent Allowance", default=0, blank=True, null=True)
    conveyance = models.PositiveIntegerField(default=0, blank=True, null=True)
    bonuses = models.PositiveIntegerField(default=0, blank=True, null=True)
    allowance = models.PositiveIntegerField(default=0, blank=True, null=True)
    medical_allowance = models.PositiveIntegerField(
        default=0, blank=True, null=True)
    tds = models.PositiveIntegerField(
        "Tax Deducted at Source (T.D.S.)", default=0, blank=True, null=True)
    esi = models.PositiveIntegerField(default=0, blank=True, null=True)
    providence_fund = models.PositiveIntegerField(
        "Provident Fund", default=0, blank=True, null=True)
    leave = models.PositiveIntegerField(default=0, blank=True, null=True)
    tax = models.PositiveIntegerField(default=0, blank=True, null=True)
    labour_welfare = models.PositiveIntegerField(
        default=0, blank=True, null=True)
    loan_repayment = models.PositiveIntegerField(
        default=0, blank=True, null=True)
    others = models.PositiveIntegerField(default=0, blank=True, null=True)

    @property
    def total_earnings(self):
        return self.basic + self.da_percent + self.hra_percent + self.conveyance + self.bonuses + self.allowance + self.medical_allowance

    @property
    def total_deductions(self):
        return self.tds + self.esi + self.providence_fund + self.leave + self.tax + self.others

    def net_pay(self):
        return self.total_earnings - self.total_deductions

    class Meta:
        """Meta definition for Payroll."""

    def get_absolute_url(self,company_id, company_staff_id,):
        return reverse('salary-detail',{'company_id':company_id,'company_staff_id':company_staff_id}, kwargs={'pk': self.pk})

        verbose_name = 'Salary'
        verbose_name_plural = 'Salaries'

    def __str__(self):
        return f"{self.employee} salary"
