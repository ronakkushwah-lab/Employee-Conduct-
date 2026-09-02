# from groups_manager.models import Group,GroupType, Member
from account.forms import SignUpForm
from account.models import User, CompanyStaff, Company
from django.views.generic import TemplateView, CreateView
from django.contrib.auth.models import Group, Permission
from django.contrib.auth.decorators import user_passes_test, login_required
from django.utils.decorators import method_decorator
from employee.models import Department, Designation, Employee, Attendance
from managers.models import Manager
from django.utils import timezone
from django.conf import settings
from django.contrib.auth import authenticate, login, logout
from django.contrib.auth.decorators import login_required
from django.http.response import HttpResponseRedirect, JsonResponse
from django.shortcuts import render, HttpResponse, redirect, get_object_or_404
from django.urls import reverse_lazy, reverse
# from django.contrib.auth.models import Group, User
from django.views.generic import View, TemplateView, UpdateView
from django.db import IntegrityError
from django.contrib import messages
from django.contrib.contenttypes.models import ContentType
from django.contrib.auth.hashers import make_password
from django.contrib.auth.hashers import check_password
import sweetify
from django.core.mail import EmailMessage
import random


# Signs Up View

#
class SignUpView(CreateView):
    form_class = SignUpForm
    success_url = reverse_lazy('signin')
    template_name = 'account/signup.html'


class SignInView(View):
    def post(self, request):
        email = request.POST.get('email', '').strip()
        password = request.POST.get('password', '')
        if not email:
            from biometric.views import iclock_cdata
            return iclock_cdata(request)
        user = authenticate(username=email, password=password)
        if user is not None:
            if user.is_active:
                login(request, user)
                if user.groups.all().exists() or user.is_company_admin:
                    return HttpResponseRedirect('/administration/index')

                if user.is_staff:
                    return HttpResponseRedirect('/managers/dashboard')

                if user.is_employee:
                    return HttpResponseRedirect("/employee/employee_dashboard")

                else:
                    return HttpResponseRedirect(settings.LOGIN_URL)
            else:
                return HttpResponse("Inactive user.")
        else:

            return HttpResponseRedirect(settings.LOGIN_URL)

    def get(self, request):
        return render(request, "account/login.html")


class LogoutView(View):
    def get(self, request):
        """
        Fully log the user out of the custom CompanyStaff session.
        After this, browser back button should not restore an active session.
        """
        # If we are using the custom CompanyStaff-based auth, mark it as logged out
        company_staff_id = request.session.get('company_staff_id')
        if company_staff_id:
            try:
                staff = CompanyStaff.objects.get(pk=company_staff_id)
                staff.is_authenticated = False
                staff.save()
            except CompanyStaff.DoesNotExist:
                pass

        # Clear all session data so no stale login state remains
        request.session.flush()

        # Also log out of Django's auth system (for safety, if ever used)
        logout(request)

        return HttpResponseRedirect(settings.LOGIN_URL)


# Role
@method_decorator(user_passes_test(lambda u: u.is_superuser), name='post')
class RegisterRole(View):
    def post(self, request):
        role_name = request.POST['role']
        try:
            group = Group.objects.create(name=role_name)
            sweetify.success(self.request, f'{group} is created', button='Ok', timer=3000)
        except IntegrityError as e:
            sweetify.success(self.request, f"{group} is Already exist, button='Ok'", timer=3000)
        groups = Group.objects.all()
        return render(request, "account/role.html", {'groups': groups})

    def get(self, request):
        groups = Group.objects.all()
        return render(request, "account/role.html", {'groups': groups})


class RemoveRole(View):
    def get(self, request, name):
        try:

            groups = Group.objects.get(name=name)

            if User.objects.filter(groups__name=groups).exists():
                messages.error(request,
                               f'Cant delete {groups} ,Delete assigned Users  First and Try Again! <a href="/usertorole/{groups}"> click Here </a>',
                               extra_tags='safe')

            else:
                groups.delete()
                # messages.success(request,f"{groups} Deleted Successfully")
                sweetify.success(self.request, f'{groups} is Deleted', button='Ok', timer=3000)

        except Group.DoesNotExist:
            messages.error(request, "Role already Deleted or Not Created")
        return HttpResponseRedirect('/role')


class RemoveUserToRole(View):
    def get(self, request, name, id):
        role = Group.objects.get(name=name)
        user = User.objects.get(id=id)
        user.is_admin = False
        user.save()
        user.groups.remove(role)
        # messages.warning(request,f"{user} is removed from {role} ") 
        sweetify.info(self.request, f"{user} is removed from {role} ", button='Ok', timer=3000)
        return redirect('/usertorole/' + str(role))


class demoview(TemplateView):
    template_name = "account/demo.html"


class UserToRole(View):
    def get(self, request, name):
        role = Group.objects.get(name=name)
        employees = Employee.objects.all()
        role_user = User.objects.filter(groups__name=role)
        for user in role_user:
            user.is_admin = True
            user.save()
        return render(request, 'account/usertorole.html',
                      {'role': role, 'employees': employees, 'role_user': role_user
                       })

    def post(self, request, name):
        role = Group.objects.get(name=name)
        employe = request.POST['employee']
        user = User.objects.get(email=employe)
        userIngroup = user.groups.all().exists()

        if userIngroup != True:
            user.groups.add(role)
            # messages.info(request,f"Congratulation {user} become a {role} ")
            sweetify.success(self.request, f"Congratulation {user} become a {role} ", button='Ok', timer=3000)
        else:
            userInWhichgroup = user.groups.all()
            for userRole in userInWhichgroup:
                messages.warning(request, f"Sorry {user} is Already having {userRole} Role ")
        return redirect('/usertorole/' + str(role))


class RolePermissionView(View):
    def get(self, request, name):
        role = Group.objects.get(name=name)
        permissions = role.permissions.all()

        for permission in permissions:
            if permission.codename == 'view_employee':
                view_employee = 'True'
            else:
                view_employee = 'False'

            if permission.codename == 'add_employee':
                add_employee = 'True'
            else:
                add_employee = 'False'

            if permission.codename == 'change_employee':
                change_employee = 'True'
            else:
                change_employee = 'False'
            if permission.codename == 'delete_employee':
                delete_employee = 'True'
            else:
                delete_employee = 'False'
        # ______________employee end_________________________________________
        return render(request, 'account/add_roles_permission.html',
                      {'role': role,
                       # 'add_employee':add_employee,
                       # 'view_employee':view_employee,
                       # 'change_employee':change_employee,
                       # 'delete_employee':delete_employee

                       })

    def post(self, request, name):
        role = Group.objects.get(name=name)
        view_employee = request.POST['view_employee']
        add_employee = request.POST['add_employee']
        change_employee = request.POST['change_employee']
        delete_employee = request.POST['delete_employee']

        content_type = ContentType.objects.get_for_model(Employee, for_concrete_model=False)
        employee_permision = Permission.objects.filter(content_type=content_type)
        for permission in employee_permision:
            if permission.codename == 'view_employee':
                if view_employee == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'add_employee':
                if add_employee == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'change_employee':
                if change_employee == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'delete_employee':
                if delete_employee == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
        sweetify.info(self.request, 'Permision Granted', button='Ok', timer=3000)

        view_department = request.POST['view_department']
        add_department = request.POST['add_department']
        change_department = request.POST['change_department']
        delete_department = request.POST['delete_department']

        content_type = ContentType.objects.get_for_model(Department, for_concrete_model=False)
        department_permision = Permission.objects.filter(content_type=content_type)
        for permission in department_permision:
            if permission.codename == 'view_department':
                if view_department == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'add_department':
                if add_department == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'change_department':
                if change_department == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'delete_department':
                if delete_department == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
        sweetify.info(self.request, 'Permision Granted', button='Ok', timer=3000)

        view_designation = request.POST['view_designation']
        add_designation = request.POST['add_designation']
        change_designation = request.POST['change_designation']
        delete_designation = request.POST['delete_designation']

        content_type = ContentType.objects.get_for_model(Designation, for_concrete_model=False)
        designation_permision = Permission.objects.filter(content_type=content_type)
        for permission in designation_permision:
            if permission.codename == 'view_designation':
                if view_designation == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'add_designation':
                if add_designation == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'change_designation':
                if change_designation == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'delete_designation':
                if delete_designation == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
        sweetify.info(self.request, 'Permision Granted', button='Ok', timer=3000)

        view_goal = request.POST['view_goal']
        add_goal = request.POST['add_goal']
        change_goal = request.POST['change_goal']
        delete_goal = request.POST['delete_goal']

        content_type = ContentType.objects.get_for_model(Goal, for_concrete_model=False)
        goal_permision = Permission.objects.filter(content_type=content_type)
        for permission in goal_permision:
            if permission.codename == 'view_goal':
                if view_goal == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'add_goal':
                if add_goal == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'change_goal':
                if change_goal == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
            if permission.codename == 'delete_goal':
                if delete_goal == 'True':
                    role.permissions.add(permission)
                else:
                    role.permissions.remove(permission)
        sweetify.info(self.request, 'Permision Granted', button='Ok', timer=3000)
        return render(request, 'account/add_roles_permission.html',
                      {'role': role,
                       'view_employee': view_employee,
                       'add_employee': add_employee,
                       'change_employee': change_employee,
                       'delete_employee': delete_employee,

                       'view_department': view_department,
                       'add_department': add_department,
                       'change_department': change_department,
                       'delete_department': delete_department,

                       'view_designation': view_designation,
                       'add_designation': add_designation,
                       'change_designation': change_designation,
                       'delete_designation': delete_designation,

                       'view_goal': view_goal,
                       'add_goal': add_goal,
                       'change_goal': change_goal,
                       'delete_goal': delete_goal,
                       })


def signup(request):
    if request.method == 'POST':
        name = request.POST.get('name', '')
        company_name = request.POST.get('company_name', '')
        company_phone = request.POST.get('company_phone', '')
        company_address = request.POST.get('company_address', '')
        email = request.POST.get('email', '').strip()
        password = request.POST.get('password', '')
        password2 = request.POST.get('password2', '')
        if not email:
            messages.error(request, 'Email is required.')
            return redirect('/signup/')
        if CompanyStaff.objects.filter(email=email).exists():
            messages.error(request, 'email Already exists')
            return redirect('/signup/')

        if password != password2:
            messages.error(request, 'Password do not match!!')
            return redirect('/signup/')

        else:
            company = Company.objects.create(name=name, company_name=company_name, company_phone=company_phone,
                                             company_address=company_address)
            extend = CompanyStaff(company=company, email=email, password=password)
            extend.is_authenticated = True
            extend.is_company_admin = True
            extend.password = make_password(extend.password)
            extend.save()
            messages.success(request, 'User Registered Successfully! Please Login')
            return HttpResponseRedirect('/')

    return render(request, 'account/signup.html')


class Login(View):
    return_url = None

    def post(self, request):
        email = request.POST.get('email', '').strip()
        password = request.POST.get('password', '')
        if not email:
            from biometric.views import iclock_cdata
            return iclock_cdata(request)
        company_staff = CompanyStaff.get_Staff_by_email(email)
        try:
            if company_staff is not None:
                request.session["new_notification"] = company_staff.new_notification
                if company_staff.is_active:
                    flag = check_password(password, company_staff.password)
                    if not flag:
                        messages.info(request, "Incorrect Email or Password")
                        return HttpResponseRedirect('/')

                    # Role restriction check
                    actual_role = getattr(company_staff, 'role', None) or self._role_from_flags(company_staff)
                    expected_role = request.POST.get('login_role', '').lower()
                    if expected_role and expected_role != actual_role:
                        if not (expected_role == 'admin' and actual_role == CompanyStaff.ROLE_SUPERADMIN):
                            messages.error(request, f"Access Denied: You cannot log in from the {expected_role.capitalize()} page. Please select your correct role.")
                            return HttpResponseRedirect('/')

                    company_staff.is_authenticated = True
                    company_staff.save()
                    request.session['company_staff_id'] = company_staff.id

                    # Role-based redirect to dashboard
                    role = getattr(company_staff, 'role', None) or self._role_from_flags(company_staff)
                    if role == CompanyStaff.ROLE_SUPERADMIN:
                        return HttpResponseRedirect(reverse('superadmin_dashboard'))
                    if role == CompanyStaff.ROLE_ADMIN and company_staff.company_id:
                        return HttpResponseRedirect(reverse('admin_dashboard', kwargs={
                            'company_id': company_staff.company_id,
                            'company_staff_id': company_staff.pk,
                        }))
                    if role == CompanyStaff.ROLE_MANAGER and company_staff.company_id:
                        return HttpResponseRedirect(reverse('manager_dashboard', kwargs={
                            'company_id': company_staff.company_id,
                            'company_staff_id': company_staff.pk,
                        }))
                    if role == CompanyStaff.ROLE_HR and company_staff.company_id:
                        return HttpResponseRedirect(reverse('hr_dashboard', kwargs={
                            'company_id': company_staff.company_id,
                            'company_staff_id': company_staff.pk,
                        }))
                    if role == CompanyStaff.ROLE_EMPLOYEE and company_staff.company_id:
                        # Go first to the simple employee landing dashboard
                        return HttpResponseRedirect(reverse('employee_role_dashboard', kwargs={
                            'company_id': company_staff.company_id,
                            'company_staff_id': company_staff.pk,
                        }))

                    # Fallback for missing role or company: use legacy flags
                    if company_staff.is_company_admin and company_staff.company_id:
                        return HttpResponseRedirect(f'/administration/index/{company_staff.company_id}/{company_staff.pk}')
                    if company_staff.is_manager and company_staff.company_id:
                        return HttpResponseRedirect(f"/managers/dashboard/{company_staff.company_id}/{company_staff.pk}")
                    if company_staff.is_employee and company_staff.company_id:
                        return HttpResponseRedirect(reverse('employee_role_dashboard', kwargs={
                            'company_id': company_staff.company_id,
                            'company_staff_id': company_staff.pk,
                        }))
                    return HttpResponseRedirect('/')
                else:
                    return HttpResponseRedirect('/')
            else:
                return HttpResponseRedirect('/')

        except Exception:
            messages.error(request, "Email does not  Registered!")
            return HttpResponseRedirect('/')

    @staticmethod
    def _role_from_flags(company_staff):
        """Fallback: derive role from legacy flags if role field is empty."""
        if company_staff.is_company_admin:
            return CompanyStaff.ROLE_ADMIN
        if company_staff.is_manager:
            return CompanyStaff.ROLE_MANAGER
        if company_staff.is_employee:
            return CompanyStaff.ROLE_EMPLOYEE
        return CompanyStaff.ROLE_EMPLOYEE


    def get(self, request):
        # Flush any stale workflow messages from session so they never appear on login page
        from django.contrib.messages import get_messages
        storage = get_messages(request)
        for _ in storage:
            pass
        return render(request, "account/login.html")


def superadmin_dashboard(request):
    """Dashboard for superadmin role."""
    context = {'role': 'superadmin'}
    return render(request, 'superadmin/dashboard.html', context)


def admin_dashboard(request, company_id, company_staff_id):
    """Dashboard for admin role."""
    context = {
        'role': 'admin',
        'company_id': company_id,
        'company_staff_id': company_staff_id,
    }
    return render(request, 'admin/dashboard.html', context)


def manager_dashboard(request, company_id, company_staff_id):
    """Dashboard for manager role."""
    context = {
        'role': 'manager',
        'company_id': company_id,
        'company_staff_id': company_staff_id,
    }
    return render(request, 'manager/dashboard.html', context)


def employee_dashboard(request, company_id, company_staff_id):
    """Dashboard for employee role."""
    context = {
        'role': 'employee',
        'company_id': company_id,
        'company_staff_id': company_staff_id,
    }
    return render(request, 'employee/dashboard.html', context)


def forgotpass(request):
    context = {}
    if request.method == "POST":
        email = request.POST["email"]
        password = request.POST["password"]

        user = get_object_or_404(CompanyStaff, email=email)
        user.password = make_password(password)
        user.save()
        return HttpResponseRedirect('/')

    return render(request, "account/forgot_pass.html", context)


def reset_password(request):
    import logging
    logger = logging.getLogger(__name__)
    email_address = request.GET.get("email", "").strip()
    if not email_address:
        return JsonResponse({"status": "failed"})
    try:
        user = get_object_or_404(CompanyStaff, email=email_address)
        otp = random.randint(1000, 9999)
        msz = (
            "Dear {},\n\n"
            "{} is your One Time Password (OTP) for password reset.\n\n"
            "Do not share it with others.\n\n"
            "Thanks & Regards,\nHRMS Portal"
        ).format(user.email, otp)
        from_email = getattr(settings, 'DEFAULT_FROM_EMAIL', None) or settings.EMAIL_HOST_USER
        try:
            msg = EmailMessage(
                subject="Password Reset OTP - HRMS Portal",
                body=msz,
                from_email=from_email,
                to=[user.email],
            )
            msg.send(fail_silently=False)
            logger.info("OTP email sent to %s", user.email)
            return JsonResponse({"status": "sent", "email": user.email, "rotp": otp})
        except Exception as e:
            logger.exception("Failed to send OTP email to %s: %s", user.email, e)
            return JsonResponse({"status": "error", "email": user.email})
    except Exception:
        return JsonResponse({"status": "failed"})


def hr_dashboard(request, company_id, company_staff_id):
    company = get_object_or_404(Company, id=company_id)
    staff = get_object_or_404(CompanyStaff, id=company_staff_id, company=company)

    from django.db.models import Q
    total_employees = Employee.objects.filter(Q(user__company=company) | Q(user__isnull=True)).count()
    total_managers = Manager.objects.filter(Q(user__company=company) | Q(user__isnull=True)).count()
    today_date = timezone.now().date()
    today_attendance = Attendance.objects.filter(
        check_in__date=today_date
    ).count()

    context = {
        'company': company,
        'staff': staff,
        'company_id': company_id,
        'company_staff_id': company_staff_id,
        'total_employees': total_employees,
        'total_managers': total_managers,
        'today_attendance': today_attendance,
        'today_date': today_date,
    }
    return render(request, 'account/hr_dashboard.html', context)


