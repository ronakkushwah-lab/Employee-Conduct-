from django.contrib import admin
from .models import Employee, Department, Entries, Attendance, Post


# Register your models here.



@admin.register(Employee)
class EmployeeAdmin(admin.ModelAdmin):
    list_display = ('employee_first_name', 'employee_last_name', 'employee_id', 'biometric_id', 'employee_email')
    search_fields = ('employee_first_name', 'employee_last_name', 'employee_id', 'biometric_id', 'employee_email')

    class Meta:
        model = Employee
        fields = '__all__'
   
@admin.register(Department)
class DepartmentAdmin(admin.ModelAdmin):
    class Meta:
        model = Department
        fields = '__all__'


admin.site.register(Entries)
admin.site.register(Attendance)
admin.site.register(Post)
