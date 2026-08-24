from django.urls import path

from . import views


app_name = 'biometric'

urlpatterns = [
    path('log/', views.bridge_punch, name='legacy_log'),
    path('attendance/biometric-punch/', views.bridge_punch, name='biometric_punch'),
    path('attendance/manual-punch/', views.manual_punch, name='manual_punch'),
    path('attendance/http-push/', views.http_push, name='http_push'),
    path('attendance/biometric-heartbeat/', views.biometric_heartbeat, name='biometric_heartbeat'),
    # Standard IClock paths (when included under /iclock/)
    path('cdata', views.iclock_cdata, name='cdata_no_slash'),
    path('cdata/', views.iclock_cdata, name='cdata'),
    path('getrequest', views.iclock_getrequest, name='getrequest_no_slash'),
    path('getrequest/', views.iclock_getrequest, name='getrequest'),
    path('devicecmd', views.iclock_devicecmd, name='devicecmd_no_slash'),
    path('devicecmd/', views.iclock_devicecmd, name='devicecmd'),
    path('registry', views.iclock_registry, name='registry_no_slash'),
    path('registry/', views.iclock_registry, name='registry'),
    # Also support prefixed paths (when included under /api/)
    path('iclock/cdata', views.iclock_cdata, name='iclock_cdata_no_slash'),
    path('iclock/cdata/', views.iclock_cdata, name='iclock_cdata'),
    path('iclock/getrequest', views.iclock_getrequest, name='iclock_getrequest_no_slash'),
    path('iclock/getrequest/', views.iclock_getrequest, name='iclock_getrequest'),
    path('iclock/devicecmd', views.iclock_devicecmd, name='iclock_devicecmd_no_slash'),
    path('iclock/devicecmd/', views.iclock_devicecmd, name='iclock_devicecmd'),
    path('iclock/registry', views.iclock_registry, name='iclock_registry_no_slash'),
    path('iclock/registry/', views.iclock_registry, name='iclock_registry'),
]
