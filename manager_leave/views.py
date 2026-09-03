from django.contrib.auth.mixins import LoginRequiredMixin, UserPassesTestMixin
from django.http import HttpResponseRedirect
from django.shortcuts import render, redirect

from django.views.generic import CreateView, DeleteView

from managers.models import Manager
from .forms import LeaveDataForm
from .models import ManagerLeave, BalanceLeave

from django.views.generic import DetailView, ListView
from django.db.models import Q


def BalanceCreateView(request,company_id, company_staff_id):
    if company_id:
        if request.method == "POST":
            balancedays = request.POST.get("balancedays")
            assign_id = request.POST.get("manager_id")
            
            # Validate that a manager is selected
            if not assign_id or assign_id == "":
                from django.contrib import messages
                messages.error(request, 'Please select a manager.')
                return render(request,"manager_leave/add-leaves-balance.html",{
                    'assigned':Manager.objects.filter(user__company__id=company_id),
                    'company_id':company_id, 
                    'company_staff_id':company_staff_id
                })
            
            assigned_to = Manager.objects.get(id=assign_id)
            # company_staff = CompanyStaff.objects.get(id=company_staff_id)
            # user = company_staff
            # emp = Employee.objects.get(user = user)

            BalanceLeave.objects.create(balancedays=balancedays, user=assigned_to)
            return redirect(f'/administration/balancelist/{company_id}/{company_staff_id}')

        else:
            return render(request,"manager_leave/add-leaves-balance.html",{'assigned':Manager.objects.filter(user__company__id=company_id),'company_id':company_id, 'company_staff_id':company_staff_id})


class BalanceDetailView(DetailView, LoginRequiredMixin):
    model = BalanceLeave


class BalanceDeleteView(DeleteView, LoginRequiredMixin, UserPassesTestMixin):
    model =  BalanceLeave
    success_url = '/administration/index/'

    def test_func(self):
        balance = self.get_object()
        return self.request.user == balance.created_by

