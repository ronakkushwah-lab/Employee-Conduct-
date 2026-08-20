from django.contrib.auth.decorators import login_required
from django.contrib.auth.models import Group
from django.http.response import HttpResponseRedirect
from django.shortcuts import render,HttpResponse,redirect
from django.urls.base import reverse
from django.views import generic
from django.views.generic import TemplateView,CreateView,ListView,UpdateView
from django.views.generic.base import View

from managers.models import Manager

from employee.models import Employee, Attendance

from django.db.models import Q


# class EmployeeSearchResultsView(ListView):
#     model = Employee
#     template_name ='administration/all-employees.html'
#     context_object_name = "Employees"
#
#     def get_queryset(self):
#         email = self.request.GET.get('employee_email')
#         name = self.request.GET.get('employee_name')
#         Employees = Employee.objects.filter(
#             Q(employee_email__icontains=email) | Q(employee_first_name__icontains=name)
#         )
#         return Employees

def EmployeeSearchResultsView(request,company_id, company_staff_id):
    if 'q' in request.GET:
        q = request.GET['q']
        multiple_q = Q(Q(user__email=q) | Q(employee_first_name__icontains=q) | Q(employee_last_name__icontains=q))
        Employees = Employee.objects.filter(multiple_q)
    else:
        Employees = Employee.objects.filter(user__company__id=company_id)
    context = {
        'Employees': Employees,
        'company_id': company_id,
        'company_staff_id': company_staff_id
    }
    return render(request, 'administration/all-employees.html', context)


# class managerSearchResultsView(ListView):
#     model = Manager
#     template_name = 'administration/manager.html'
#     context_object_name = "manager"
#
#     def get_queryset(self):
#         email = self.request.GET.get('manager_email')  # new
#         name = self.request.GET.get('manager_name')  # new
#         manager = Manager.objects.filter(
#             Q(manager_email__icontains=email) | Q(manager_first_name__icontains=name)
#         )
#         return manager


def managerSearchResultsView(request):
    if 'q' in request.GET:
        q = request.GET['q']
        print('-' * 10)
        print(q)
        print('-' * 10)
        multiple_q = Q(Q(user__email=q) | Q(manager_first_name__icontains=q) | Q(manager_last_name__icontains=q))
        Managers = Manager.objects.filter(multiple_q)
    else:
        Managers = Manager.objects.all()
    context = {
        'manager': Managers
    }
    print('managers:', Managers)
    return render(request, 'administration/all-manager.html', context)


def AttendanceSearchResultsView(request):
    if 'q' in request.GET:
        q = request.GET['q']
        multiple_q = Q(Q(employee_email__icontains=q) | Q(employee_first_name__icontains=q) | Q(employee_last_name__icontains=q))
        Employees = Employee.objects.filter(multiple_q)
    else:
        Employees = Employee.objects.all()
    context = {
        'Employees': Employees
    }
    return render(request, 'administration/all-employees.html', context)


