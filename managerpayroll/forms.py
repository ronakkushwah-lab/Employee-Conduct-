from django import forms
from .models import Salary
from managers.models import Manager


class DateInput(forms.DateInput):
    input_type = 'date'


class SalaryForm(forms.ModelForm):
    def __init__(self, company_id, *args, **kwargs):
        super(SalaryForm, self).__init__(*args, **kwargs)
        self.fields['manager'].queryset = Manager.objects.filter(user__company_id=company_id)

    class Meta:
        model = Salary
        fields = '__all__'

        widgets = {
            'month': DateInput(format='%Y-%m-%d'),
        }




