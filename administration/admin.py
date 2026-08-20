from django.contrib import admin
from .models import Client,Lead, Task,notification,holiday,MTask,Asign,ManagerNotification,EmailNotification


# Register your models here.
# -------------------------------------Client---------------------------------------------------
@admin.register(Client)
class ClientAdmin(admin.ModelAdmin):
    class Meta:
        model = Client
        fields = '__all__'


# -------------------------------------Leads---------------------------------------------------
@admin.register(Lead)
class LeadAdmin(admin.ModelAdmin):
    class Meta:
        model = Lead
        fields = '__all__'
# -------------------------------------/Leads---------------------------------------------------
admin.site.register(Task)
admin.site.register(notification)
admin.site.register(holiday)
admin.site.register(MTask)
admin.site.register(Asign)
admin.site.register(ManagerNotification)


@admin.register(EmailNotification)
class EmailNotificationAdmin(admin.ModelAdmin):
    list_display = ('sender_email', 'sender_name', 'subject', 'received_date', 'is_read')
    list_filter = ('is_read', 'received_date')
    search_fields = ('sender_email', 'sender_name', 'subject', 'body_preview')
    ordering = ('-received_date',)
