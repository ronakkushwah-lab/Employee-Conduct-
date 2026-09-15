from django.urls import path
from . import views
from .views import InvoiceListView, createInvoice, GeneratePdf

urlpatterns = [

    path('invoices/<int:company_id>/<int:company_staff_id>', InvoiceListView.as_view(), name='invoices'),
    path('invoice-detail/<int:company_id>/<int:company_staff_id>/<id>', views.view_PDF, name='invoice-detail'),
    path('invoice-create/<int:company_id>/<int:company_staff_id>', views.createInvoice, name="invoice-create"),
    path('invoice-download/<int:company_id>/<int:company_staff_id>/<id>', GeneratePdf.as_view(), name='invoice-download'),
    path('invoice_remove/<int:company_id>/<int:company_staff_id>/<id>', views.InvoiceRemove.as_view(), name='invoice_remove')
]