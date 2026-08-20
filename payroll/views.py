from django.shortcuts import render, get_object_or_404, redirect
from django.views import generic

from account.models import CompanyStaff
from employee.models import Employee
from .models import Salary
from .forms import SalaryForm
from django.views.generic import View, DetailView, UpdateView, TemplateView
from django.http import HttpResponseRedirect
from django.urls import reverse
from django.contrib import messages
from .utils import render_to_pdf, render_slip_html
from .helpers import number_to_words
from django.http import HttpResponse
from datetime import datetime


def getForm(request):
    salary = Salary.objects.all()
    form = SalaryForm()
    context = {'salary': salary, 'form': form}
    return render(request, 'payroll/employee-salary.html', context)


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

    def get(self, request,company_id, company_staff_id):
        salary = Salary.objects.filter(employee__user__company__id=company_id)
        form = SalaryForm(company_id)
        context = {'salary': salary, 'form': form,'company_id':company_id, 'company_staff_id':company_staff_id}
        return render(request, 'payroll/employee-salary.html', context)

    def post(self, request,company_id, company_staff_id):
        if company_id:
            form = SalaryForm(company_id,request.POST,request.FILES)
            if request.method == 'POST':
                if form.is_valid():
                    form.save()
                    messages.info(request, "Salary was successfully created")
                return redirect(f'/payroll/salary/{company_id}/{company_staff_id}')


def SalaryDetailView(request, company_id, company_staff_id, id=None,):
    # getting the template
    salary = get_object_or_404(Salary, id=id)

    context = {
        "id": salary.id,
        "employee": salary.employee,
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
        "total_earnings": salary.total_earnings,
        "total_deductions": salary.total_deductions,
        "net_pay": salary.net_pay,
        'company_id': company_id,
        'company_staff_id': company_staff_id,

    }
    return render(request, "payroll/employee-payslip.html", context)


class SalaryRemove(View):
    def get(self, request,company_id, company_staff_id, id):
        if company_id:
            salary = Salary.objects.get(id=id)
            print(salary)
            salary.delete()
            messages.success(request, f"{salary} deleted successfully")
            return redirect(f'/payroll/salary/{company_id}/{company_staff_id}')


class Update_salary_View(UpdateView):
    model = Salary
    fields = "__all__"
    context_object_name = "salary_update"
    template_name = 'payroll/employee-salary.html'
    success_url = ("/payroll/salary/")


class GeneratePdf(View):
    def get(self,request,company_id, company_staff_id,id=None,*args, **kwargs):
        # getting the template
        salary = get_object_or_404(Salary, id=id)
        print(salary)

        # Calculate payslip number (sequence starting from 001 for this employee)
        employee_salaries = Salary.objects.filter(employee=salary.employee).order_by('month', 'id')
        payslip_number = 1
        for idx, sal in enumerate(employee_salaries, start=1):
            if sal.id == salary.id:
                payslip_number = idx
                break

        # Convert net pay to words
        try:
            net_pay_value = salary.net_pay if not callable(salary.net_pay) else salary.net_pay()
            net_pay_words = number_to_words(net_pay_value) if net_pay_value else "Zero Only"
        except Exception as e:
            print(f"Error converting net pay to words: {e}")
            try:
                net_pay_value = salary.net_pay if not callable(salary.net_pay) else salary.net_pay()
            except:
                net_pay_value = 0
            net_pay_words = "Zero Only"

        context = {
            "object": salary,
            "id": id,
            "payslip_number": payslip_number,  # Sequential number starting from 1
            "employee":salary.employee,
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
            "net_pay":net_pay_value,
            "net_pay_words": net_pay_words,
            'company_id': company_id,
            'company_staff_id': company_staff_id,



        }

        # View mode: open slip as HTML page in tab (no print dialog). Download mode: PDF attachment.
        if request.GET.get('download') == '1':
            pdf = render_to_pdf(context)
            if pdf is None:
                return HttpResponse("Error: Failed to generate PDF", status=500)
            if pdf.get('Content-Type', '').startswith('application/pdf'):
                pdf['Content-Disposition'] = 'attachment; filename="salary-slip.pdf"'
            return pdf

        # Always return HTML so tab opens formatted slip; user clicks Download on page to get PDF
        context['download_url'] = request.build_absolute_uri() + ('&' if request.GET else '?') + 'download=1'
        return render_slip_html(context)


class CreateSalaryView(generic.CreateView):
    model = Salary
    fields = ('employee', 'month', 'basic', 'da_percent', 'hra_percent', 'conveyance','bonuses','allowance','medical_allowance','tds','esi','providence_fund','leave','tax','labour_welfare','loan_repayment','others')
    template_name = "payroll/employee-salary.html"
    success_url = ('/payroll/salary')


def All_Employee_List_View(request):
    AllEmployee = Employee.objects.filter(employee_status="Active")
    return render(request, "payroll/employee-salary.html", {'Employees': AllEmployee})

