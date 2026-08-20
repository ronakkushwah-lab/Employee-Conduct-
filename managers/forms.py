from django import forms

class managerForm(forms.Form):
    manager_first_name = forms.CharField(max_length=30, required=False, help_text='Optional') 
    manager_last_name = forms.CharField(max_length=30, required=False, help_text='Optional')
    manager_email = forms.EmailField(max_length=30, required=False, help_text='Optional')
    manager_joining_date = forms.DateField()
    manager_department = forms.CharField(max_length=30, required=False, help_text='Optional')
    manager_id = forms.CharField(max_length=30, required=False, help_text='Optional')
    manager_phone = forms.CharField(max_length=30, required=False, help_text='Optional')
    manager_password = forms.CharField(max_length=30, required=False, help_text='Optional')
    manager_confirm_password = forms.CharField(max_length=30, required=False, help_text='Optional')

# --------------------------------------------Department------------------------------------------

class FormSettings(forms.ModelForm):
    def __init__(self, *args, **kwargs):
        super(FormSettings, self).__init__(*args, **kwargs)
        for field in self.visible_fields():
            field.field.widget.attrs['class'] = 'form-control'

