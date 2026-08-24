
from django.db import models

from django.contrib.auth import get_user_model
from django.conf import settings
from django.utils import timezone
from datetime import timedelta, datetime
from django.core.exceptions import ValidationError
from django.urls import reverse
from django.db.models.signals import pre_save, post_save
from django.shortcuts import HttpResponseRedirect, reverse
from django.urls import reverse
import os
from django.utils.translation import gettext_lazy as _
# from django.contrib.auth.models import get_user_model

# -------------------------------Employee Model--------------------------------------------------------------------------------
from account.models import Company,CompanyStaff
# from managers.models import Manager
# import managers.models.Manager
from django.db import models
User = get_user_model()
Gendar = (
    ('Male', 'Male'),
    ('Female', 'Female'),
)

Marital = (
    ('Single', 'Single'),
    ('Married', 'Married'),
)

employee_status = (
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
    'Android Developer': 'Android Developer'
}


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


class Department(models.Model):
    company = models.ForeignKey(Company, on_delete=models.CASCADE, null=True, blank=True)
    department_name = models.CharField(max_length=100, null=True, blank=True)

    def __str__(self):
        return self.department_name

    @property
    def formatted_id(self):
        """Returns Department ID in DEP-XXX format"""
        return f"DEP-{self.id:03d}"

    def to_json(self):
        depatment_details_dict = {
            'id': self.id,
            'department_name': self.department_name,
        }
        return depatment_details_dict


class Employee(models.Model):
    user = models.OneToOneField(CompanyStaff, on_delete=models.CASCADE, default=True)
    employee_first_name = models.CharField(max_length=100)
    employee_last_name = models.CharField(max_length=100)
    employee_email = models.EmailField(max_length=100)
    employee_joining_date = models.DateField(max_length=50)
    employee_department = models.ForeignKey(Department, on_delete=models.CASCADE, default=True)
    employee_designation = models.CharField(max_length=100)
    employee_id = models.CharField(max_length=100)
    biometric_id = models.CharField(max_length=100, unique=True, null=True, blank=True)
    employee_phone = models.CharField(max_length=100, null=True)
    employee_salary = models.CharField(max_length=100, default=350000)
    employee_birth_date = models.CharField(max_length=100, null=True)
    employee_gender = models.CharField(max_length=50, null=True, choices=Gendar)
    employee_address = models.CharField(max_length=50, null=True)
    employee_pin_code = models.CharField(max_length=50, null=True)
    employee_state = models.CharField(max_length=50, null=True)
    employee_country = models.CharField(max_length=50, null=True)
    employee_reports_to = models.ForeignKey(to='managers.Manager', on_delete=models.CASCADE, default=True)
    employee_image = models.FileField(upload_to='media/', blank=True)
    employee_created_date = models.DateTimeField(auto_now=True)
    employee_status = models.CharField(max_length=32, choices=employee_status, default='Active')
    employee_tel = models.CharField(max_length=50, null=True)
    employee_nationality = models.CharField(max_length=50, null=True)
    employee_marital_status = models.CharField(max_length=50, null=True, choices=Marital)
    employee_father = models.CharField(max_length=50, null=True)
    employee_mother = models.CharField(max_length=50, null=True)
    employee_emergency_primary_name = models.CharField(max_length=50, null=True)
    employee_emergency_primary_relationship = models.CharField(max_length=50, null=True)
    employee_emergency_primary_phone1 = models.CharField(max_length=50, null=True)
    employee_emergency_primary_phone2 = models.CharField(max_length=50, null=True)
    employee_education_institution = models.CharField(max_length=50, null=True)
    employee_education_subject = models.CharField(max_length=50, null=True)
    employee_education_starting_date = models.CharField(max_length=50, null=True)
    employee_education_complete_date = models.CharField(max_length=50, null=True)
    employee_education_degree = models.CharField(max_length=50, null=True)
    employee_education_grade = models.CharField(max_length=50, null=True)
    employee_experience_company_name = models.CharField(max_length=50, null=True)
    employee_experience_company_location = models.CharField(max_length=50, null=True)
    employee_experience_company_job_position = models.CharField(max_length=50, null=True)
    employee_experience_company_period_from = models.CharField(max_length=50, null=True)
    employee_experience_company_period_to = models.CharField(max_length=50, null=True)

    def __str__(self):
        return self.employee_email

    def get_absolute_url(self):
        return HttpResponseRedirect(reverse('update_employees'))


    def to_json(self):
        employee_details_dict = {
            'employee_first_name': self.employee_first_name,
            'employee_last_name': self.employee_last_name,
            'employee_department': self.employee_department.department_name,
            'employee_image': self.employee_image.url if self.employee_image else None,
            'employee_gender': self.employee_gender,
            'employee_address': self.employee_address,
            'employee_phone': self.employee_phone,
            'employee_id': self.employee_id,
            'employee_status': self.employee_status,
            'employee_email': self.employee_email,
            'employee_joining_date': self.employee_joining_date,
            'employee_birth_date': self.employee_birth_date,
            'employee_father': self.employee_father,
            'employee_mother': self.employee_mother,
            'employee_nationality': self.employee_nationality,
            'employee_state': self.employee_state,
            'employee_country': self.employee_country,
            'employee_pin_code': self.employee_pin_code,
            'employee_education_degree': self.employee_education_degree,
            'employee_marital_status': self.employee_marital_status,
            'employee_education_subject': self.employee_education_subject,
            'employee_education_institution': self.employee_education_institution,
            'employee_education_grade': self.employee_education_grade,
            'employee_education_starting_date': self.employee_education_starting_date,
            'employee_education_complete_date': self.employee_education_complete_date,
            'employee_emergency_primary_relationship': self.employee_emergency_primary_relationship,
            'employee_emergency_primary_name': self.employee_emergency_primary_name,
            'employee_emergency_primary_phone1': self.employee_emergency_primary_phone1,
            'employee_experience_company_name': self.employee_experience_company_name,
            'employee_experience_company_location': self.employee_experience_company_location,
            'employee_experience_company_job_position': self.employee_experience_company_job_position,
            'employee_experience_company_period_from': self.employee_experience_company_period_from,
            'employee_experience_company_period_to': self.employee_experience_company_period_to,
            'employee_reports_to': self.employee_reports_to_id if self.employee_reports_to_id else None,
            'biometric_id': self.biometric_id or ''
        }
        return employee_details_dict


    @property
    def formatted_employee_id(self):
        """
        Always show full employee ID with 'EIC-' prefix (e.g. EIC-001, EIC-002).
        If employee_id is empty or only 'EIC-', fallback to EIC-{pk}.
        """
        eid = (self.employee_id or "").strip()
        if not eid or eid.upper() in ("EIC-", "EIC"):
            return f"EIC-{self.pk:03d}"
        if eid.upper().startswith("EIC-"):
            return eid
        return f"EIC-{eid}"

    @property
    def get_full_name(self):
        fullname = ''
        firstname = self.employee_first_name
        lastname = self.employee_last_name

        if firstname and lastname:
            fullname = firstname + ' ' + lastname
            return fullname
        return firstname or lastname or ''


# -------------------------------/Employee Model---------------------------------------------------------------------------------


# ------------------------------------Depaartment---------------------------------------------------------------------------------


# ------------------------------------/Depaartment--------------------------------------------------------------------------------
# ------------------------------------Designation---------------------------------------------------------------------------------
class Designation(models.Model):
    Designation_Name = models.CharField(max_length=100)
    Department_Name = models.ForeignKey(Department, on_delete=models.CASCADE)

    def __str__(self):
        return self.Designation_Name


class Entries(models.Model):
    '''The Task dataclass to store the task in database'''
    user = models.ForeignKey(Employee, on_delete=models.CASCADE, null=True,blank=True)
    title = models.CharField(max_length=70,blank=True,null=True)
    start_time = models.DateTimeField(validators=[validate_start_time],blank=False)
    end_time = models.DateTimeField(validators=[validate_end_time],blank=False)
    created_date = models.DateField(default=timezone.now, blank=True)
    project = models.CharField(max_length=500, null=True,blank=False)
    task = models.CharField(max_length=500, null=True,blank=False)
    blocker_name = models.CharField(max_length=500, null=True, blank=True)
    attachment = models.FileField(upload_to='timesheet_attachments/', null=True, blank=True)

    assigned_to = models.ForeignKey(
        to='managers.Manager',
        null=True,
        blank=False,
        related_name="_assigned_to",
        on_delete=models.CASCADE,
    )

    # def __str__(self):
    #     return self.user

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

    def to_json(self):
        entry_details_dict = {
            'id': self.id,
            'start_time': self.start_time,
            'end_time': self.end_time,
            'task': self.task,
            'project': self.project,
            'blocker_name': self.blocker_name,
            'attachment_url': self.attachment.url if self.attachment else '',
            'attachment_name': self.attachment.name.split('/')[-1] if self.attachment else '',
            'total_duration': self.total_duration,
            'assigned_to': self.assigned_to.manager_email
        }
        return entry_details_dict


def pre_save_entry_handler(sender, instance, *args, **kwargs):
    """
    Raise Error when a Start time of a Entry > End time of a Entry
    """
    if instance.start_time >= instance.end_time:
        raise ValidationError("Start time should be less than End Time")


pre_save.connect(pre_save_entry_handler, Entries)


class Attendance(models.Model):
    check_in = models.DateTimeField()
    check_out = models.DateTimeField(blank=True, null=True)
    employee = models.ForeignKey(Employee, null=True, on_delete=models.CASCADE)
    source = models.CharField(max_length=50, default='manual', null=False, blank=False)
    updated = models.DateTimeField(auto_now=True, auto_now_add=False,null=True)
    created = models.DateTimeField(auto_now=False, auto_now_add=True,null=True)

    class Meta:
        ordering = ['-created']  # recent objects


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


class Post(models.Model):
    experience_letter = models.FileField(verbose_name=_('Experience Letter'),null=True, blank=False, upload_to='Files')
    offer_letter = models.FileField(verbose_name=_('Offer Letter'),null=True, blank=False, upload_to='Files')
    education_certificate = models.FileField(verbose_name=_('Education Certificate'),null=True, blank=False, upload_to='Files')
    skill_certificate = models.FileField(verbose_name=_('Skill Certificate'),null=True, blank=False, upload_to='Files')
    date_posted = models.DateTimeField(default=timezone.now)
    user = models.ForeignKey(Employee,
                             null=True,
                             blank=True,
                             on_delete=models.CASCADE, )

    class Meta:
        verbose_name = _('Post')
        verbose_name_plural = _('Posts')
        ordering = ['-date_posted']  # recent objects

    def extension(self):
        name, extension = os.path.splitext(
            self.experience_letter.name or self.offer_letter.name or self.education_certificate or self.skill_certificate)
        return extension

    def get_absolute_url(self):
        return reverse('post-detail', kwargs={'pk': self.pk})

    def to_json(self):
        attendance_details_dict = {
            'id': self.id,
            'experience_letter': self.experience_letter.url if self.experience_letter else None,
            'offer_letter': self.offer_letter.url if self.offer_letter else None,
            'education_certificate': self.education_certificate.url if self.education_certificate else None,
            'skill_certificate': self.skill_certificate.url if self.skill_certificate else None,
        }
        return attendance_details_dict
    
    def get_file_type(self, file_field):
        """Get file type extension"""
        if not file_field:
            return None
        import os
        name, extension = os.path.splitext(file_field.name)
        return extension.lower() if extension else None
    
    def is_image(self, file_field):
        """Check if file is an image"""
        ext = self.get_file_type(file_field)
        return ext in ['.jpg', '.jpeg', '.png', '.gif', '.bmp', '.webp'] if ext else False
    
    def is_pdf(self, file_field):
        """Check if file is a PDF"""
        ext = self.get_file_type(file_field)
        return ext == '.pdf' if ext else False
    
    def is_document(self, file_field):
        """Check if file is a document (doc, docx)"""
        ext = self.get_file_type(file_field)
        return ext in ['.doc', '.docx'] if ext else False


# # class EmployeeDocument(models.Model):
# #     employee = models.ForeignKey(Employee, on_delete=models.CASCADE)



class EmployeeDocument(models.Model):
    employee = models.ForeignKey(
        Employee,
        on_delete=models.CASCADE,
        related_name="documents"
    )

    experience_letter = models.FileField(upload_to="employee_docs/", null=True, blank=True)
    offer_letter = models.FileField(upload_to="employee_docs/", null=True, blank=True)
    education_certificate = models.FileField(upload_to="employee_docs/", null=True, blank=True)
    skill_certificate = models.FileField(upload_to="employee_docs/", null=True, blank=True)

    created_at = models.DateTimeField(auto_now_add=True)

    def __str__(self):
        return f"Documents of {self.employee.employee_first_name} {self.employee.employee_last_name}"
        
