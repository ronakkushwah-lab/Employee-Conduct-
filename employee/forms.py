from django import forms
from django.forms import DateInput
from django.forms import ModelForm
from .models import Employee, Department, Designation, Entries
from django.contrib.auth import get_user_model
from django.conf import settings
from django.utils import timezone
from datetime import timedelta, datetime
from django.core.exceptions import ValidationError
from django.urls import reverse
from django.db.models.signals import pre_save, post_save
from django.shortcuts import HttpResponseRedirect, reverse
from django.urls import reverse
import os





class EmployeeForm(forms.Form):
    employee_first_name = forms.CharField(max_length=30, required=False, help_text='Optional')
    employee_last_name = forms.CharField(max_length=30, required=False, help_text='Optional')
    employee_email = forms.EmailField(max_length=30, required=False, help_text='Optional')
    employee_joining_date = forms.DateField()
    employee_department = forms.CharField(max_length=30, required=False, help_text='Optional')
    employee_id = forms.CharField(max_length=30, required=False, help_text='Optional')
    employee_phone = forms.CharField(max_length=30, required=False, help_text='Optional')
    employee_password = forms.CharField(max_length=30, required=False, help_text='Optional')
    employee_confirm_password = forms.CharField(max_length=30, required=False, help_text='Optional')


# --------------------------------------------Department------------------------------------------

class FormSettings(forms.ModelForm):
    def __init__(self, *args, **kwargs):
        super(FormSettings, self).__init__(*args, **kwargs)
        for field in self.visible_fields():
            field.field.widget.attrs['class'] = 'form-control'


class DepartmentForm(forms.ModelForm):
    class Meta:
        model = Department
        fields = "__all__"


# --------------------------------------------Department------------------------------------------
# --------------------------------------------designation------------------------------------------
class DesignationForm(forms.ModelForm):
    class Meta:
        model = Designation
        fields = "__all__"


# --------------------------------------------/designation------------------------------------------


class EntryCreationForm(forms.ModelForm):

    class Meta:
        model = Entries
        exclude = ['user', 'created_date','title','start_time']





