from sys import prefix
from django.shortcuts import render, HttpResponseRedirect, redirect, get_object_or_404
from django.views import generic

from account.models import CompanyStaff
from .models import Invoice, LineItem
from django.views.generic import TemplateView,CreateView,ListView,UpdateView, DeleteView
from django.views.generic.base import View
from django.contrib import messages
from employee.models import Employee
from datetime import datetime

from .forms import LineItemForm, LineItemFormset, InvoiceForm
from django.http import HttpResponse
from django.core import serializers
import json
from django.urls import reverse
import sweetify
from django.db.models import Sum

from administration import constants
from .utils import render_to_pdf


class InvoiceListView(View):
    def dispatch(self, request, company_id, company_staff_id, *args, **kwargs):
        print('Dispatch function called')
        company_staff = CompanyStaff.objects.filter(pk=company_staff_id)
        if company_staff.exists():
            if company_staff.first().is_authenticated:
                return super().dispatch(request, company_id, company_staff_id, *args, **kwargs)
            else:
                return redirect('/')
        else:
            return redirect('/')

    def get(self,company_id, company_staff_id):
        invoices = Invoice.objects.filter(company__id=company_id)
        context = {
            "invoices": invoices,
            'company_id': company_id,
            'company_staff_id': company_staff_id

        }

        return render(self.request, 'management/invoices.html', context)

    def post(self, request,company_id, company_staff_id):
        if company_id:
            # import pdb;pdb.set_trace()
            invoice_ids = request.POST.getlist("invoice_id")
            invoice_ids = list(map(int, invoice_ids))

            update_status_for_invoices = int(request.POST['status'])
            invoices = Invoice.objects.filter(id__in=invoice_ids)
            # import pdb;pdb.set_trace()
            if update_status_for_invoices == 0:
                invoices.update(status=False)
            else:
                invoices.update(status=True)
            return redirect(f'/management/invoice-create/{company_id}/{company_staff_id}')


def createInvoice(request,company_id, company_staff_id):
    """
    Invoice Generator page it will have Functionality to create new invoices,
    this will be protected view, only admin has the authority to read and make
    changes here.
    """
    if company_id:
        global formset, form
        heading_message = 'Formset Demo'
        if request.method == 'GET':
            all_invoices = Invoice.objects.filter(company__id=company_id)
            formset = LineItemFormset(request.GET or None)
            form = InvoiceForm(request.GET or None)
        elif request.method == 'POST':
            formset = LineItemFormset(request.POST)
            form = InvoiceForm(request.POST)

            if form.is_valid():
                invoice = Invoice.objects.create(client=form.data["client"],
                                                 client_email=form.data["client_email"],
                                                 billing_address=form.data["billing_address"],
                                                 date=form.data["date"],
                                                 due_date=form.data["due_date"],
                                                 project=form.data["project"],
                                                 company_id = company_id,

                                                 )

            if formset.is_valid():
                total = 0
                for form in formset:
                    service = form.cleaned_data.get('service')
                    description = form.cleaned_data.get('description')
                    quantity = form.cleaned_data.get('quantity')
                    rate = form.cleaned_data.get('rate')
                    if service and description and quantity and rate:
                        amount = float(rate) * float(quantity)
                        total += amount
                        LineItem(client=invoice,
                                 service=service,
                                 description=description,
                                 quantity=quantity,
                                 rate=rate,
                                 amount=amount).save()
                invoice.total_amount = total
                invoice.save()
            return redirect(f'/management/invoice-create/{company_id}/{company_staff_id}')
                # try:
                #     invoice_pdf_obj = GeneratePdf()
                #     invoice_pdf_obj = invoice_pdf_obj.get(request, invoice.id,company_id, company_staff_id)
                # except Exception as e:
                #     print(f"********{e}********")
                # # return redirect('/management/invoices/')
                # return invoice_pdf_obj

        context = {
            "title": "Invoice Generator",
            "all_invoices": all_invoices,
            "formset": formset,
            "form": form,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
        }
        return render(request, 'management/invoices.html', context)


def view_PDF(request, company_id, company_staff_id,id=None):
    if company_id:
        invoice = get_object_or_404(Invoice, id=id)
        lineitem = invoice.lineitem_set.all()

        context = {
            "company": {
                "name": constants.COMPANY_NAME,
                "address": constants.COMPANY_ADDRESS,
                "phone": constants.COMPANY_PHONE,
                "email": constants.COMPAMY_EMAIL,
            },
            "invoice_id": invoice.id,
            "invoice_total": invoice.total_amount,
            "client": invoice.client,
            "client_email": invoice.client_email,
            "date": invoice.date,
            "due_date": invoice.due_date,
            "billing_address": invoice.billing_address,
            "project": invoice.project,
            'company_id': company_id,
            'company_staff_id': company_staff_id,
        }
        if len(lineitem) != 0:
            context['lineitem'] = {
                "description": lineitem[0].description,
                "rate": lineitem[0].rate,
                "quantity": lineitem[0].quantity,
                "amount": lineitem[0].amount
            }

        return render(request, 'management/invoice-view.html', context)


class GeneratePdf(View):
    def dispatch(self, request, company_id, company_staff_id, *args, **kwargs):
        print('Dispatch function called')
        company_staff = CompanyStaff.objects.filter(pk=company_staff_id)
        if company_staff.exists():
            if company_staff.first().is_authenticated:
                return super().dispatch(request, company_id, company_staff_id, *args, **kwargs)
            else:
                return redirect('/')
        else:
            return redirect('/')

    def get(self,request,company_id, company_staff_id,id=None):
        # getting the template
        if company_id:
            invoice = get_object_or_404(Invoice, id=id)
            lineitem = invoice.lineitem_set.all()

            context = {
                "company": {
                    "name": constants.COMPANY_NAME,
                    "address": constants.COMPANY_ADDRESS,
                    "phone": constants.COMPANY_PHONE,
                    "email": constants.COMPAMY_EMAIL,
                },
                "invoice_id": invoice.id,
                "invoice_total": invoice.total_amount,
                "client": invoice.client,
                "client_email": invoice.client_email,
                "date": invoice.date,
                "due_date": invoice.due_date,
                "billing_address": invoice.billing_address,
                "project": invoice.project,
                'company_id': company_id,
                'company_staff_id': company_staff_id,
            }
            if len(lineitem) != 0:
                context['lineitem'] = {
                    "description": lineitem[0].description,
                    "rate": lineitem[0].rate,
                    "quantity": lineitem[0].quantity,
                    "amount": lineitem[0].amount
                    }
            pdf = render_to_pdf(context)
            return HttpResponse(pdf, content_type='application/pdf')


class InvoiceRemove(View):
     def get(self,request,company_id, company_staff_id,id):
        if company_id:
            invoice=Invoice.objects.get(id=id)
            invoice.delete()
            messages.success(request,f"{invoice} deleted successfully")
            return redirect(f'/management/invoice-create/{company_id}/{company_staff_id}')


