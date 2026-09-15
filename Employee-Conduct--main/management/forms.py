from django import forms
from django.forms import formset_factory

from .models import  Invoice, LineItem
from dstt import settings




class Invoice_form(forms.ModelForm):
    def __init__(self, company_id, *args, **kwargs):
        super(Invoice_form, self).__init__(*args, **kwargs)
        self.fields['invoice'].queryset = Invoice.objects.filter(id__in=company_id)

    class Meta:
        model = Invoice
        fields = "__all__"

class Line_form(forms.ModelForm):
    class Meta:
        model = LineItem
        fields = "__all__"


class InvoiceForm(forms.Form):
    # fields = ['customer', 'message']
    client = forms.CharField(
        label='client',
        widget=forms.TextInput(attrs={
            'class': 'form-control',
            'placeholder': 'client/Company Name',
            'rows': 1
        })
    )
    client_email = forms.CharField(
        label='Client Email',
        widget=forms.TextInput(attrs={
            'class': 'form-control',
            'placeholder': 'client@company.com',
            'rows': 1
        })
    )
    billing_address = forms.CharField(
        label='Billing Address',
        widget=forms.TextInput(attrs={
            'class': 'form-control',
            'placeholder': '',
            'rows': 1
        })
    )
    project = forms.CharField(
        label='project',
        widget=forms.TextInput(attrs={
            'class': 'form-control',
            'placeholder': 'project',
            'rows': 1
        })
    )


class LineItemForm(forms.Form):
    service = forms.CharField(
        label='Service/Product',
        widget=forms.TextInput(attrs={
            'id': 'project',
            'class': 'form-control input',
            'placeholder': 'Service Name'
        })
    )
    description = forms.CharField(
        label='Description',
        widget=forms.TextInput(attrs={
            'id': 'address',
            'class': 'form-control input',
            'placeholder': 'Enter Book Name here',
            "rows": 1
        })
    )
    quantity = forms.IntegerField(
        label='Qty',
        widget=forms.TextInput(attrs={
            'id': 'project',
            'class': 'form-control input quantity',
            'placeholder': 'Quantity'
        })  # quantity should not be less than one
    )
    rate = forms.DecimalField(
        label='Rate Rs',
        widget=forms.TextInput(attrs={
            'id': 'project',
            'class': 'form-control input rate',
            'placeholder': 'Rate'
        })
    )


LineItemFormset = formset_factory(LineItemForm, extra=1)