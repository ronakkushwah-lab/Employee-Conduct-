from django.utils.deprecation import MiddlewareMixin
from django.shortcuts import render, redirect
from django.urls import reverse


class LoginCheckMiddleWare(MiddlewareMixin):
    
    def process_view(self, request, view_func, view_args, view_kwargs):
        modulename = view_func.__module__
        # print(modulename)
        company = request.company

        #Check whether the user is logged in or not
        if company.is_authenticated:
            if company.is_company_admin:
                if modulename == "administration.views" or  modulename == "account.views" or modulename == "performances.views" or modulename == "employee.views" or modulename == "management.views"  :
                    pass
                else:
                    return redirect("/administration/index")

            elif company.is_manager == True:
                if modulename == "managers.views" or modulename == "account.views" or modulename == "django.views.static":
                    pass
                else:
                    return redirect("/managers/dashboard")

            elif company.is_employee ==True:
                if modulename == "employee.views"  or   modulename == "account.views" or modulename == "django.views.static":
                    pass
                else:
                    return redirect("/employee/employee_dashboard")
            

            else:
                return redirect("signin")

        else:
            if request.path == reverse("signin") or request.path == reverse("signin"):
                pass
            else:
                return redirect("signin")
