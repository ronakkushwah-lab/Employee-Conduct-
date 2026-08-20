from django.shortcuts import render, get_object_or_404, redirect

from account.models import CompanyStaff
from employee.models import Employee
from .models import Salary
from .forms import SalaryForm
from django.views.generic import View, DetailView, UpdateView, TemplateView
from django.http import HttpResponseRedirect
from django.urls import reverse
from django.contrib import messages
from .utils import render_to_pdf
from django.http import HttpResponse
from datetime import datetime


class SalaryView(View):
    def dispatch(self, request, company_id, company_staff_id, *args,**kwargs):
        print('Dispatch function called')
        company_staff = CompanyStaff.objects.filter(pk=company_staff_id)
        if company_staff.exists():
            if company_staff.first().is_authenticated:
                return super().dispatch(request, company_id, company_staff_id, *args, **kwargs)
            else:
                return redirect('/')
        else:
            return redirect('/')
    
    def get(self, request, company_id, company_staff_id):
        if company_id:
            salary = Salary.objects.filter(manager__user__company__id=company_id)
            form = SalaryForm(company_id)
            print('form',form)
            context = {'salary': salary, 'form': form, 'company_id':company_id, 'company_staff_id':company_staff_id}
            return render(request, 'managerpayroll/manager-salary.html', context)

    def post(self, request, company_id, company_staff_id):
        if company_id:
            form = SalaryForm(company_id,request.POST ,request.FILES)
            if request.method == 'POST':
                if form.is_valid():
                    form.save()
                    messages.info(request, "Salary was successfully created")
                return redirect(f'/managerpayroll/msalary/{company_id}/{company_staff_id}')


def SalaryDetailView(request, company_id, company_staff_id, id=None,):

    salary = get_object_or_404(Salary, id=id)

    context = {
            "id": salary.id,
            "manager":salary.manager,
            "month": salary.month,
            "basic": salary.basic,
            "da_percent": salary.da_percent,
            "hra_percent": salary.hra_percent,
            "conveyance": salary.conveyance,
            "bonuses": salary.bonuses,
            "allowance": salary.allowance,
            "medical_allowance": salary.medical_allowance,
            "tds": salary.tds,
            "esi": salary.esi,
            "providence_fund": salary.providence_fund,
            "leave": salary.leave,
            "tax": salary.tax,
            "total_earnings":salary.total_earnings,
            "total_deductions":salary.total_deductions,
            "net_pay":salary.net_pay,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

    }
    return render(request,"managerpayroll/manager-payslip.html", context)


class SalaryRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            salary = Salary.objects.get(id=id)
            print(salary)
            salary.delete()
            messages.success(request, f"{salary} deleted successfully")
            return redirect(f'/managerpayroll/msalary/{company_id}/{company_staff_id}')


class Update_salary_View(UpdateView):
    model = Salary
    fields = "__all__"
    context_object_name = "salary_update"
    template_name = 'managerpayroll/manager-salary.html'
    success_url = ("/managerpayroll/salary/")


class GeneratePdf(View):
    def get(self,request,company_id, company_staff_id,id=None,*args, **kwargs):
        # getting the template
        salary = get_object_or_404(Salary, id=id)
        context = {
            "id": salary.id,
            "manager":salary.manager,
            "month": salary.month,
            "basic": salary.basic,
            "da_percent": salary.da_percent,
            "hra_percent": salary.hra_percent,
            "conveyance": salary.conveyance,
            "bonuses": salary.bonuses,
            "allowance": salary.allowance,
            "medical_allowance": salary.medical_allowance,
            "tds": salary.tds,
            "esi": salary.esi,
            "providence_fund": salary.providence_fund,
            "leave": salary.leave,
            "tax": salary.tax,
            "total_earnings":salary.total_earnings,
            "total_deductions":salary.total_deductions,
            "net_pay":salary.net_pay,
            'company_id': company_id,
            'company_staff_id': company_staff_id,

        }
        # NOTE:
        # Server-side PDF generation (xhtml2pdf/reportlab) can fail on some Windows
        # machines due to DLL load restrictions. For manager slips we always render
        # the HTML view and trigger the existing client-side html2pdf download.
        context['auto_download'] = True
        return render(request, "managerpayroll/manager-payslip.html", context)
    
    

