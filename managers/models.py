import os

from django.db import models

from django.contrib.auth import get_user_model
from django.conf import settings
from django.utils import timezone
from datetime import timedelta, datetime
from django.core.exceptions import ValidationError
from django.urls import reverse
from django.db.models.signals import pre_save, post_save
from django.shortcuts import HttpResponseRedirect, reverse


# from django.contrib.auth.models import get_user_model

# -------------------------------manager Model--------------------------------------------------------------------------------
from account.models import CompanyStaff
# from employee.models import Department
# import employee.models.Department
from django.db import models

from employee.models import Employee


def validate_start_time(value):
    """
    Validate that a Entry should have a starting date & time in present
    or Future (with 5 Minute negotation)
    """
    if value < timezone.now() - timedelta(minutes=5):
        raise ValidationError(
            "Starting Time should be in present or Future")


def validate_end_time(value):
    """
    Validate that a Entry should have a ending less than 6 months
    """
    print("validating end time")
    if value > timezone.now() + timedelta(days=31 * 6):
        raise ValidationError(
            "Ending Time should be less than 6 months")


User = get_user_model()
Gendar = (
    ('Male', 'Male'),
    ('Female', 'Female'),
)
manager_status = (
    ('Active', 'Active'),
    ('Inactive', 'Inactive')
)

role_choices = {
    'Web Developer': 'Web Developer',
    'Software Engineer': 'Software Engineer',
    'Software Tester': 'Software Tester',
    'Frontend Developer': 'Frontend Developer',
    'UI/UX Developer': 'UI/UX Developer',
    'Team Leader': 'Team Leader',
    'IOS Developer': 'IOS Developer',
    'Android Developer': 'Android Developer',
    'Python Developer': 'Python Developer',
}


class Manager(models.Model):
    user = models.OneToOneField(CompanyStaff, on_delete=models.CASCADE, default=True)
    manager_first_name = models.CharField(max_length=100)
    manager_last_name = models.CharField(max_length=100)
    manager_email = models.EmailField(max_length=100)
    manager_joining_date = models.DateField(max_length=50)
    manager_department = models.ForeignKey(to = 'employee.Department', on_delete=models.CASCADE, null=True)
    # manager_department = models.CharField(max_length=100, null=True,)
    manager_designation = models.CharField(max_length=100)
    manager_id = models.CharField(max_length=100)
    biometric_id = models.CharField(max_length=100, unique=True, null=True, blank=True)
    manager_phone = models.CharField(max_length=100, null=True)
    manager_salary = models.CharField(max_length=100, default=350000)
    manager_birth_date = models.CharField(max_length=100, null=True)
    manager_gender = models.CharField(max_length=50, null=True, choices=Gendar)
    manager_father = models.CharField(max_length=100,null=True)
    manager_mother = models.CharField(max_length=100,null=True)
    manager_address = models.CharField(max_length=50, null=True)
    manager_pin_code = models.CharField(max_length=50, null=True)
    manager_state = models.CharField(max_length=50, null=True)
    manager_country = models.CharField(max_length=50, null=True)
    manager_image = models.FileField(upload_to='media/', blank=True)
    manager_created_date = models.DateTimeField(auto_now=True)
    manager_status = models.CharField(max_length=32, choices=manager_status, default='Active')
    manager_tel = models.CharField(max_length=50, null=True)
    manager_nationality = models.CharField(max_length=50, null=True)
    manager_religion = models.CharField(max_length=50, null=True)
    manager_marital_status = models.CharField(max_length=50, null=True)
    manager_emergency_primary_name = models.CharField(max_length=50, null=True)
    manager_emergency_primary_relationship = models.CharField(max_length=50, null=True)
    manager_emergency_primary_phone1 = models.CharField(max_length=50, null=True)
    manager_emergency_primary_phone2 = models.CharField(max_length=50, null=True)
    manager_education_institution = models.CharField(max_length=50, null=True)
    manager_education_subject = models.CharField(max_length=50, null=True)
    manager_education_starting_date = models.CharField(max_length=50, null=True)
    manager_education_complete_date = models.CharField(max_length=50, null=True)
    manager_education_degree = models.CharField(max_length=50, null=True)
    manager_education_grade = models.CharField(max_length=50, null=True)
    manager_experience_company_name = models.CharField(max_length=50, null=True)
    manager_experience_company_location = models.CharField(max_length=50, null=True)
    manager_experience_company_job_position = models.CharField(max_length=50, null=True)
    manager_experience_company_period_from = models.CharField(max_length=50, null=True)
    manager_experience_company_period_to = models.CharField(max_length=50, null=True)

    def __str__(self):
        return self.manager_email

    def get_absolute_url(self):
        return HttpResponseRedirect(reverse('update_managers'))

    @property
    def formatted_manager_id(self):
        """
        Always show full manager ID with 'EIC-' prefix (e.g. EIC-001, EIC-002).
        If manager_id is empty or only "EIC-", fallback to EIC-{pk}.
        """
        mid = (self.manager_id or "").strip()
        # Empty or only "EIC-" (no number) → use primary key so ID is always full
        if not mid or mid.upper() in ("EIC-", "EIC"):
            return f"EIC-{self.pk:03d}"
        if mid.upper().startswith("EIC-"):
            return mid
    @property
    def avatar_url(self):
        if self.manager_image:
            return self.manager_image.url
        if self.manager_gender and str(self.manager_gender).strip().lower() == 'female':
            return '/static/asets/images/dummy-woman.png'
        return '/static/asets/images/dummy-man.png'

    @property
    def get_full_name(self):
        fullname = ''
        firstname = self.manager_first_name
        lastname = self.manager_last_name

        if firstname and lastname is None:
            fullname = firstname + ' ' + lastname
            return fullname
        return

    def to_json(self):
        manager_details_dict = {
            'manager_first_name': self.manager_first_name,
            'manager_last_name': self.manager_last_name,
            'manager_department': self.manager_department.department_name,
            'manager_image': self.manager_image.url if self.manager_image else None,
            'manager_gender': self.manager_gender,
            'manager_address': self.manager_address,
            'manager_phone': self.manager_phone,
            'manager_id': self.manager_id,
            'manager_status': self.manager_status,
            'manager_email': self.manager_email,
            'manager_joining_date': self.manager_joining_date,
            'manager_birth_date': self.manager_birth_date,
            'manager_father': self.manager_father,
            'manager_mother': self.manager_mother,
            'manager_nationality': self.manager_nationality,
            'manager_state': self.manager_state,
            'manager_country': self.manager_country,
            'manager_pin_code': self.manager_pin_code,
            'manager_education_degree': self.manager_education_degree,
            'manager_marital_status': self.manager_marital_status,
            'manager_education_subject': self.manager_education_subject,
            'manager_education_institution': self.manager_education_institution,
            'manager_education_grade': self.manager_education_grade,
            'manager_education_starting_date': self.manager_education_starting_date,
            'manager_education_complete_date': self.manager_education_complete_date,
            'manager_emergency_primary_relationship': self.manager_emergency_primary_relationship,
            'manager_emergency_primary_name': self.manager_emergency_primary_name,
            'manager_emergency_primary_phone1': self.manager_emergency_primary_phone1,
            'manager_experience_company_name': self.manager_experience_company_name,
            'manager_experience_company_location': self.manager_experience_company_location,
            'manager_experience_company_job_position': self.manager_experience_company_job_position,
            'manager_experience_company_period_from': self.manager_experience_company_period_from,
            'manager_experience_company_period_to': self.manager_experience_company_period_to,
            'biometric_id': self.biometric_id or ''
        }
        return manager_details_dict


class ManagerEntry(models.Model):
    """
    Represent record a log created by user to track projects.
    """
    name = models.CharField(max_length=250,blank=False)
    start_time = models.DateTimeField(validators=[validate_start_time],blank=False)
    end_time = models.DateTimeField(validators=[validate_end_time],blank=False)
    user = models.ForeignKey(
        settings.AUTH_USER_MODEL,
        on_delete=models.CASCADE,
        related_name='user_entry'
    )
    project = models.CharField(max_length=500, null=True,blank=False)
    activity = models.CharField(max_length=500, null=True,blank=False)

    class Meta:
        verbose_name_plural = "Entry"

    def __str__(self):
        return self.user

    def clean(self):
        """
        Raise Error when a Start time of a Entry > End time of a Entry
        """
        if self.start_time >= self.end_time:
            raise ValidationError("Start time should be less than End Time")

    @property
    def total_duration(self):
        """
        Entry's property for the total duration alloted
        """
        return self.end_time - self.start_time

    @property
    def time_left(self):
        """
        Entry's property for the total duration left
        """
        time = self.end_time - timezone.now().replace(microsecond=0)
        if time < timedelta(seconds=1):
            time = timedelta(seconds=0)
        return time

    @property
    def format_time_left(self):
        """
        Format the time left into Day-Hr-Min-Sec
        """
        time = self.end_time + timedelta(hours=5, minutes=30)
        return time.strftime("%m/%d/%Y %H:%M:%S")

    @property
    def is_active(self):
        """
        Check the Expiry of the Entry
        """
        return timezone.now() < self.end_time

    def time_left_sec(self):
        """
        Format the time left in seconds.
        """
        td = self.time_left
        seconds = td.seconds + td.days * 24 * 3600
        return seconds


def pre_save_entry_handler(sender, instance, *args, **kwargs):
    """
    Raise Error when a Start time of a Entry > End time of a Entry
    """
    if instance.start_time >= instance.end_time:
        raise ValidationError("Start time should be less than End Time")


pre_save.connect(pre_save_entry_handler, ManagerEntry)


class ManagerAttendance(models.Model):
    check_in = models.DateTimeField()
    check_out = models.DateTimeField(blank=True, null=True)
    manager = models.ForeignKey(Manager, null=True, on_delete=models.CASCADE)


    @property
    def formatted_working_hours(self):
        if self.check_in and self.check_out:
            total_seconds = int((self.check_out - self.check_in).total_seconds())
            if total_seconds > 0:
                hours = total_seconds // 3600
                minutes = (total_seconds % 3600) // 60
                return f"{hours}h {minutes}m"
            return "0m"
        elif self.check_in and not self.check_out:
            return "In Progress"
        return "-"

    @property
    def working_hour(self):
        working_hour = None
        if self.check_in and self.check_out:
            check_in_hour = self.check_in.hour
            check_out_hour = self.check_out.hour
            working_hour = check_out_hour - check_in_hour
        return working_hour


    @property
    def regularization_required(self):
        if self.check_in and self.check_out:
            check_in_hour = self.check_in.hour
            check_out_hour = self.check_out.hour
            working_hour = check_out_hour - check_in_hour
            if working_hour < 9 :
                return True
            else:
                return False
        return False


    def to_json(self):
        attendance_details_dict = {
            'id': self.id,
            'check_in': self.check_in,
            'check_out': self.check_out,
        }
        return attendance_details_dict


class ManagerPost(models.Model):
    experience_letter = models.FileField(null=True, blank=False, upload_to='Files')
    offer_letter = models.FileField(null=True, blank=False, upload_to='Files')
    education_certificate = models.FileField(null=True, blank=False, upload_to='Files')
    skill_certificate = models.FileField(null=True, blank=False, upload_to='Files')
    date_posted = models.DateTimeField(default=timezone.now)
    user = models.ForeignKey(Manager,
                             null=True,
                             blank=True,
                             on_delete=models.CASCADE,)

    def extension(self):
        name, extension = os.path.splitext(
            self.experience_letter.name and self.offer_letter.name and self.education_certificate and self.skill_certificate)
        return extension

    def get_absolute_url(self):
        return reverse('mpost-detail', kwargs={'pk': self.pk})

    def to_json(self):
        post_details_dict = {
            'id': self.id,
            'experience_letter': self.experience_letter.url if self.experience_letter else None,
            'offer_letter': self.experience_letter.url if self.offer_letter else None,
            'education_certificate': self.experience_letter.url if self.education_certificate else None,
            'skill_certificate': self.experience_letter.url if self.skill_certificate else None,

        }
        return post_details_dict


class EmployeeNotification(models.Model):
    user = models.ForeignKey(Employee, on_delete=models.CASCADE, null=True, blank=True)
    notifications = models.CharField(max_length=500, blank=False)
    is_read = models.BooleanField(default=False, null=False, blank=False)

    def to_json(self):
        notification_details_dict = {
            'id': self.id,
            'notifications': self.notifications,
        }
        return notification_details_dict

    def __str__(self):
        return ('{0} - {1}'.format(self.id, self.user))
