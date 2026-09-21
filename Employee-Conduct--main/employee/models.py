
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
    Validate that a timesheet entry start time is reasonable (e.g. not more than 1 year in the past).
    """
    if value:
        if timezone.is_naive(value):
            value = timezone.make_aware(value, timezone.get_current_timezone())
        if value < timezone.now() - timedelta(days=365):
            raise ValidationError(
                "Starting Time cannot be more than 1 year in the past")


def validate_end_time(value):
    """
    Validate that a timesheet entry end time is reasonable (e.g. within 6 months into future).
    """
    if value:
        if timezone.is_naive(value):
            value = timezone.make_aware(value, timezone.get_current_timezone())
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
    employee_reports_to = models.ForeignKey(
        to='managers.Manager',
        on_delete=models.SET_NULL,
        null=True,
        blank=True,
    )
    employee_image = models.FileField(upload_to='media/', blank=True)
    avatar_base64 = models.TextField(blank=True, null=True)
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

    @property
    def avatar_url(self):
        # 1. Base64 avatar saved in database (permanent on Neon)
        if self.avatar_base64 and self.avatar_base64.strip():
            return self.avatar_base64.strip()

        # 2. Uploaded image file (only if physical file exists on disk/storage)
        if self.employee_image:
            try:
                if self.employee_image.storage.exists(self.employee_image.name):
                    return self.employee_image.url
            except Exception:
                pass

        # 3. Fallback to gender-based default avatar
        if self.employee_gender and str(self.employee_gender).strip().lower() == 'female':
            return '/static/asets/images/dummy-woman.png'
        return '/static/asets/images/dummy-man.png'

    def save(self, *args, **kwargs):
        # Auto-convert uploaded image file to avatar_base64 if not already present
        if self.employee_image and not self.avatar_base64:
            try:
                import base64
                import mimetypes
                self.employee_image.open('rb')
                content = self.employee_image.read()
                if content:
                    content_type = mimetypes.guess_type(self.employee_image.name)[0] or 'image/jpeg'
                    b64_str = base64.b64encode(content).decode('utf-8')
                    self.avatar_base64 = f"data:{content_type};base64,{b64_str}"
            except Exception:
                pass
        if 'update_fields' in kwargs and kwargs['update_fields'] is not None:
            kwargs['update_fields'] = set(kwargs['update_fields'])
            if self.avatar_base64:
                kwargs['update_fields'].add('avatar_base64')
        super().save(*args, **kwargs)

    def __str__(self):
        return self.employee_email

    def get_absolute_url(self):
        return HttpResponseRedirect(reverse('update_employees'))


    def to_json(self):
        employee_details_dict = {
            'employee_first_name': self.employee_first_name,
            'employee_last_name': self.employee_last_name,
            'employee_department': self.employee_department.department_name if self.employee_department else '',
            'employee_department_id': self.employee_department.id if self.employee_department else '',
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
            'biometric_id': self.biometric_id or '',
            'employee_role': getattr(self.user, 'role', 'employee') if self.user else 'employee',
            'is_hr': bool(self.user and (self.user.role == 'hr' or getattr(self.user, 'is_hr', False)))
        }
        return employee_details_dict

    @property
    def is_hr(self):
        return bool(self.user and (self.user.role == 'hr' or getattr(self.user, 'is_hr', False)))

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


def format_duration(td):
    """
    Format a timedelta object into a human-readable string like '1 day 2 hrs', '8 hrs 30 mins', '45 mins'.
    """
    if not td or not isinstance(td, timedelta):
        return "0 mins"
    total_seconds = int(td.total_seconds())
    if total_seconds <= 0:
        return "0 mins"
    days = total_seconds // 86400
    remaining_secs = total_seconds % 86400
    hours = remaining_secs // 3600
    minutes = (remaining_secs % 3600) // 60

    parts = []
    if days > 0:
        parts.append(f"{days} day{'s' if days > 1 else ''}")
    if hours > 0:
        parts.append(f"{hours} hr{'s' if hours > 1 else ''}")
    if minutes > 0:
        parts.append(f"{minutes} min{'s' if minutes != 1 else ''}")
    return " ".join(parts) if parts else "0 mins"


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

    STATUS_PENDING = 'pending'
    STATUS_APPROVED = 'approved'
    STATUS_REJECTED = 'rejected'

    STATUS_CHOICES = (
        (STATUS_PENDING, 'Pending'),
        (STATUS_APPROVED, 'Approved'),
        (STATUS_REJECTED, 'Rejected'),
    )

    status = models.CharField(max_length=20, choices=STATUS_CHOICES, default=STATUS_PENDING)
    is_approved = models.BooleanField(default=False)
    approved_by = models.ForeignKey(
        to='managers.Manager',
        null=True,
        blank=True,
        related_name="approved_timesheet_entries",
        on_delete=models.SET_NULL,
    )
    approved_at = models.DateTimeField(null=True, blank=True)
    rejection_reason = models.TextField(null=True, blank=True)

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
        Raise Error when a Start time of a Entry >= End time of a Entry
        """
        if self.start_time and self.end_time:
            st = self.start_time
            et = self.end_time
            if timezone.is_naive(st):
                st = timezone.make_aware(st, timezone.get_current_timezone())
            if timezone.is_naive(et):
                et = timezone.make_aware(et, timezone.get_current_timezone())
            if st >= et:
                raise ValidationError("Start time should be less than End Time")

    @property
    def total_duration(self):
        """
        Entry's property for the total duration allotted
        """
        if not self.start_time or not self.end_time:
            return timedelta(seconds=0)
        st = self.start_time
        et = self.end_time
        if timezone.is_naive(st):
            st = timezone.make_aware(st, timezone.get_current_timezone())
        if timezone.is_naive(et):
            et = timezone.make_aware(et, timezone.get_current_timezone())
        diff = et - st
        return diff if diff > timedelta(seconds=0) else timedelta(seconds=0)

    @property
    def formatted_duration(self):
        """
        Human-readable formatted total duration
        """
        return format_duration(self.total_duration)

    @property
    def time_left(self):
        """
        Entry's property for the total duration left
        """
        if not self.end_time:
            return timedelta(seconds=0)
        et = self.end_time
        if timezone.is_naive(et):
            et = timezone.make_aware(et, timezone.get_current_timezone())
        now = timezone.now().replace(microsecond=0)
        time = et - now
        if time < timedelta(seconds=1):
            time = timedelta(seconds=0)
        return time

    @property
    def format_time_left(self):
        """
        Format the time left into Day-Hr-Min-Sec
        """
        if not self.end_time:
            return ""
        et = self.end_time
        if timezone.is_naive(et):
            et = timezone.make_aware(et, timezone.get_current_timezone())
        return et.strftime("%m/%d/%Y %H:%M:%S")

    def approve(self, manager):
        self.status = self.STATUS_APPROVED
        self.is_approved = True
        self.approved_by = manager
        self.approved_at = timezone.now()
        self.rejection_reason = None
        self.save()

    def reject(self, manager, reason=""):
        self.status = self.STATUS_REJECTED
        self.is_approved = False
        self.approved_by = manager
        self.approved_at = timezone.now()
        self.rejection_reason = reason
        self.save()

    def to_json(self):
        assigned_name = ''
        if self.assigned_to:
            assigned_name = f"{self.assigned_to.manager_first_name} {self.assigned_to.manager_last_name}".strip() or self.assigned_to.manager_email
        
        approved_by_name = ''
        if self.approved_by:
            approved_by_name = f"{self.approved_by.manager_first_name} {self.approved_by.manager_last_name}".strip() or self.approved_by.manager_email

        approved_at_str = ''
        if self.approved_at:
            local_approved_at = timezone.localtime(self.approved_at) if timezone.is_aware(self.approved_at) else self.approved_at
            approved_at_str = local_approved_at.strftime('%b %d, %Y %I:%M %p')

        start_time_str = ''
        if self.start_time:
            local_st = timezone.localtime(self.start_time) if timezone.is_aware(self.start_time) else self.start_time
            start_time_str = local_st.strftime('%Y-%m-%d %I:%M %p')

        end_time_str = ''
        if self.end_time:
            local_et = timezone.localtime(self.end_time) if timezone.is_aware(self.end_time) else self.end_time
            end_time_str = local_et.strftime('%Y-%m-%d %I:%M %p')

        attachment_url = self.attachment.url if self.attachment else ''
        attachment_name = os.path.basename(self.attachment.name) if self.attachment else ''

        user_name = ''
        if self.user:
            user_name = f"{self.user.employee_first_name} {self.user.employee_last_name}".strip() or getattr(self.user, 'employee_email', '')

        return {
            'id': self.id,
            'title': self.title or '',
            'project': self.project or '',
            'task': self.task or '',
            'blocker_name': self.blocker_name or '',
            'start_time': start_time_str,
            'end_time': end_time_str,
            'total_duration': self.formatted_duration,
            'attachment_url': attachment_url,
            'attachment_name': attachment_name,
            'assigned_to': assigned_name,
            'user': user_name,
            'status': self.status,
            'status_display': self.get_status_display(),
            'is_approved': self.is_approved,
            'approved_by': approved_by_name,
            'approved_at': approved_at_str,
            'rejection_reason': self.rejection_reason or '',
        }

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


pre_save.connect(pre_save_entry_handler, Entries)


class Attendance(models.Model):
    check_in = models.DateTimeField()
    check_out = models.DateTimeField(blank=True, null=True)
    employee = models.ForeignKey(Employee, null=True, on_delete=models.CASCADE)
    source = models.CharField(max_length=50, default='manual', null=False, blank=False)
    updated = models.DateTimeField(auto_now=True, auto_now_add=False,null=True)
    created = models.DateTimeField(auto_now=False, auto_now_add=True,null=True)

    class Meta:
        ordering = ['-check_in']  # chronological by attendance punch time


    @property
    def formatted_working_hours(self):
        if self.check_in and self.check_out:
            total_seconds = int((self.check_out - self.check_in).total_seconds())
            if total_seconds > 0:
                hours = total_seconds // 3600
                minutes = (total_seconds % 3600) // 60
                return f"{hours:02d}.{minutes:02d}"
            return "00.00"
        elif self.check_in and not self.check_out:
            return "In Progress"
        return "-"

    @property
    def is_short_hours(self):
        if self.check_in and self.check_out:
            total_seconds = int((self.check_out - self.check_in).total_seconds())
            return 0 < total_seconds < 30600  # Less than 8.5 hours (30600 seconds)
        return False

    @property
    def shortfall_working_hours(self):
        if self.check_in and self.check_out:
            total_seconds = int((self.check_out - self.check_in).total_seconds())
            if 0 < total_seconds < 30600:
                short_sec = 30600 - total_seconds
                hours = short_sec // 3600
                minutes = (short_sec % 3600) // 60
                return f"{hours:02d}.{minutes:02d}"
        return "00.00"

    @property
    def shortfall_human(self):
        if self.check_in and self.check_out:
            total_seconds = int((self.check_out - self.check_in).total_seconds())
            if 0 < total_seconds < 30600:
                short_sec = 30600 - total_seconds
                hours = short_sec // 3600
                minutes = (short_sec % 3600) // 60
                if hours > 0 and minutes > 0:
                    return f"{hours}h {minutes}m"
                elif hours > 0:
                    return f"{hours}h"
                else:
                    return f"{minutes}m"
        return "0m"

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
    experience_letter = models.FileField(verbose_name=_('Experience Letter'), null=True, blank=True, upload_to='Files')
    offer_letter = models.FileField(verbose_name=_('Offer Letter'), null=True, blank=True, upload_to='Files')
    education_certificate = models.FileField(verbose_name=_('Education Certificate'), null=True, blank=True, upload_to='Files')
    skill_certificate = models.FileField(verbose_name=_('Skill Certificate'), null=True, blank=True, upload_to='Files')
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
        for f in [self.experience_letter, self.offer_letter, self.education_certificate, self.skill_certificate]:
            if f and hasattr(f, 'name') and f.name:
                _, ext = os.path.splitext(f.name)
                return ext
        return ''

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
        
