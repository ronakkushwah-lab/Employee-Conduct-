from django import forms

from employee.models import Employee
from .models import Salary


class DateInput(forms.DateInput):
    input_type = 'date'


class SalaryForm(forms.ModelForm):
    def __init__(self, company_id, *args, **kwargs):
        super(SalaryForm, self).__init__(*args, **kwargs)
        self.fields['employee'].queryset = Employee.objects.filter(user__company_id=company_id)


    class Meta:
        model = Salary
        fields = '__all__'

        widgets = {
            'month': DateInput(format='%Y-%m-%d'),
        }
