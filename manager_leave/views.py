from django.contrib.auth.mixins import LoginRequiredMixin, UserPassesTestMixin
from django.http import HttpResponseRedirect
from django.shortcuts import render, redirect

from django.views.generic import CreateView, DeleteView

from managers.models import Manager
from .forms import LeaveDataForm
from .models import ManagerLeave, BalanceLeave

from django.views.generic import DetailView, ListView
from django.db.models import Q


from employee.models import Employee
from leave.models import BalanceLeaves
from django.contrib import messages


def BalanceCreateView(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            balancedays = request.POST.get("balancedays")
            user_id = request.POST.get("user_id") or request.POST.get("manager_id") or request.POST.get("employee_id")
            
            # Validate that a staff member is selected
            if not user_id or user_id == "":
                messages.error(request, 'Please select an employee or manager.')
                return render(request, "manager_leave/add-leaves-balance.html", {
                    'assigned_managers': Manager.objects.filter(user__company__id=company_id),
                    'assigned_employees': Employee.objects.filter(user__company__id=company_id),
                    'company_id': company_id, 
                    'company_staff_id': company_staff_id
                })
            
            if str(user_id).startswith('emp_'):
                clean_id = str(user_id).replace('emp_', '')
                emp = Employee.objects.get(id=clean_id)
                BalanceLeaves.objects.create(balancedays=int(balancedays), user=emp)
                messages.success(request, f'Leave balance of {balancedays} day(s) assigned to Employee {emp.employee_first_name} {emp.employee_last_name}!')
            else:
                clean_id = str(user_id).replace('mgr_', '')
                mgr = Manager.objects.get(id=clean_id)
                BalanceLeave.objects.create(balancedays=int(balancedays), user=mgr)
                messages.success(request, f'Leave balance of {balancedays} day(s) assigned to Manager {mgr.manager_first_name} {mgr.manager_last_name}!')

            return redirect(f'/administration/balancelist/{company_id}/{company_staff_id}')

        else:
            return render(request, "manager_leave/add-leaves-balance.html", {
                'assigned_managers': Manager.objects.filter(user__company__id=company_id),
                'assigned_employees': Employee.objects.filter(user__company__id=company_id),
                'company_id': company_id, 
                'company_staff_id': company_staff_id
            })


class BalanceDetailView(DetailView, LoginRequiredMixin):
    model = BalanceLeave


class BalanceDeleteView(DeleteView, LoginRequiredMixin, UserPassesTestMixin):
    model =  BalanceLeave
    success_url = '/administration/index/'

    def test_func(self):
        balance = self.get_object()
        return self.request.user == balance.created_by

