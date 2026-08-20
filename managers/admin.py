from django.contrib import admin

# Register your models here.
from .models import Manager,ManagerEntry,ManagerPost,ManagerAttendance,EmployeeNotification


# Register your models here.


@admin.register(Manager)
class ManagerAdmin(admin.ModelAdmin):
    list_display = ('manager_first_name', 'manager_last_name', 'manager_id', 'biometric_id', 'manager_email')
    search_fields = ('manager_first_name', 'manager_last_name', 'manager_id', 'biometric_id', 'manager_email')

    class Meta:
        model = Manager
        fields = '__all__'


admin.site.register(ManagerAttendance)
admin.site.register(ManagerEntry)
admin.site.register(ManagerPost)
admin.site.register(EmployeeNotification)
