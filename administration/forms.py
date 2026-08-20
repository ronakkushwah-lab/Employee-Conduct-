from django import forms
from django.forms import ModelForm, DateInput

from employee.models import Attendance, Employee
from .models import Client, Lead, Task
from django.contrib.auth import get_user_model
from employee.models import Employee



# from .models import EmployeeDocument


User = get_user_model()


class DateInput(forms.DateInput):
    input_type = 'date'


# --------------------------------------Client----------------------------------------------------
class ClientForm(forms.ModelForm):
    class Meta:
        model = Client
        fields = "__all__"


# --------------------------------------Lead----------------------------------------------------
class LeadForm(forms.ModelForm):
    class Meta:
        model = Lead
        fields = "__all__"


# --------------------------------------/Lead----------------------------------------------------

class AttendanceForm(forms.ModelForm):
    class Meta:
        model = Attendance
        fields = '__all__'

        # widgets = {
        #     'month': DateInput(format='%Y-%m-%d'),
        # }

class TaskForm(forms.ModelForm):
    class Meta:
        model = Task
        fields = ['title', 'description', 'assigned_to']

    def __init__(self, *args, **kwargs):
        super().__init__(*args, **kwargs)
        if self.instance and self.instance.pk and self.instance.assigned_to_id:
            self.fields['assigned_to'].widget.attrs['readonly'] = True
            self.fields['assigned_to'].widget.attrs['disabled'] = True
            self.fields['assigned_to'].required = False

    def clean_assigned_to(self):
        value = self.cleaned_data.get('assigned_to')
        if self.instance and self.instance.pk and self.instance.assigned_to_id:
            return self.instance.assigned_to
        return value



class EmployeeForm(forms.ModelForm):
    class Meta:
        model = Employee
        fields = '__all__'



# class AllDocumentForm(forms.ModelForm):
#     class Meta:
#         model = EmployeeDocument
#         fields = ['experience_letter', 'joining_letter', 'resignation_letter', 'other_document']

#     def __init__(self, company_id, *args, **kwargs):
#         super().__init__(*args, **kwargs)
#         self.company_id = company_id


# --------------------------------------Employee Document Form (like Manager)----------------------------------------------------
from employee.models import Post

class EmployeeDocumentForm(forms.ModelForm):
    """Form for uploading employee documents - similar to manager documents"""
    class Meta:
        model = Post
        fields = ['experience_letter', 'offer_letter', 'education_certificate', 'skill_certificate']
        widgets = {
            'experience_letter': forms.FileInput(attrs={'class': 'form-control', 'accept': '.pdf,.doc,.docx,.jpg,.jpeg,.png'}),
            'offer_letter': forms.FileInput(attrs={'class': 'form-control', 'accept': '.pdf,.doc,.docx,.jpg,.jpeg,.png'}),
            'education_certificate': forms.FileInput(attrs={'class': 'form-control', 'accept': '.pdf,.doc,.docx,.jpg,.jpeg,.png'}),
            'skill_certificate': forms.FileInput(attrs={'class': 'form-control', 'accept': '.pdf,.doc,.docx,.jpg,.jpeg,.png'}),
        }
    
    def __init__(self, company_id=None, *args, **kwargs):
        super().__init__(*args, **kwargs)
        self.company_id = company_id
        # Make all fields optional (user can upload any combination)
        self.fields['experience_letter'].required = False
        self.fields['offer_letter'].required = False
        self.fields['education_certificate'].required = False
        self.fields['skill_certificate'].required = False






