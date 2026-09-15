from django import forms
from .models import ManagerLeave
import datetime
from django.forms import ModelForm


class LeaveCreationForm(forms.ModelForm):
    reason = forms.CharField(required=False, widget=forms.Textarea(attrs={'rows': 4, 'cols': 40}))

    class Meta:
        model = ManagerLeave
        exclude = ['user', 'balancedays','defaultdays', 'status', 'is_approved', 'updated', 'created']

    def clean_enddate(self):
        enddate = self.cleaned_data['enddate']
        startdate = self.cleaned_data['startdate']
        today_date = datetime.date.today()

        if (startdate or enddate) < today_date:  # both dates must not be in the past
            raise forms.ValidationError("Selected dates are incorrect,please select again")

        elif startdate >= enddate:  # TRUE -> FUTURE DATE > PAST DATE,FALSE other wise
            raise forms.ValidationError("Selected dates are wrong")

        return enddate


class LeaveDataForm(ModelForm):

    class Meta:
        model = ManagerLeave
        exclude = ['status', 'is_approved', 'updated', 'created','reason','leavetype']
