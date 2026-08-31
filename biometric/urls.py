from django.urls import path

from . import views


app_name = 'biometric'

urlpatterns = [
    path('log/', views.bridge_punch, name='legacy_log'),
    path('attendance/biometric-punch/', views.bridge_punch, name='biometric_punch'),
    path('attendance/manual-punch/', views.manual_punch, name='manual_punch'),
    path('attendance/http-push/', views.http_push, name='http_push'),
    path('attendance/biometric-heartbeat/', views.biometric_heartbeat, name='biometric_heartbeat'),
    # Direct api/ prefixed routes
    path('api/attendance/biometric-punch/', views.bridge_punch, name='api_biometric_punch'),
    path('api/attendance/manual-punch/', views.manual_punch, name='api_manual_punch'),
    path('api/attendance/http-push/', views.http_push, name='api_http_push'),
    path('api/attendance/biometric-heartbeat/', views.biometric_heartbeat, name='api_biometric_heartbeat'),
    # Standard IClock paths (root and /iclock/)
    path('cdata', views.iclock_cdata, name='cdata_no_slash'),
    path('cdata/', views.iclock_cdata, name='cdata'),
    path('getrequest', views.iclock_getrequest, name='getrequest_no_slash'),
    path('getrequest/', views.iclock_getrequest, name='getrequest'),
    path('devicecmd', views.iclock_devicecmd, name='devicecmd_no_slash'),
    path('devicecmd/', views.iclock_devicecmd, name='devicecmd'),
    path('registry', views.iclock_registry, name='registry_no_slash'),
    path('registry/', views.iclock_registry, name='registry'),
    path('fdata', views.iclock_cdata, name='fdata_no_slash'),
    path('fdata/', views.iclock_cdata, name='fdata'),
    path('querydata', views.iclock_cdata, name='querydata_no_slash'),
    path('querydata/', views.iclock_cdata, name='querydata'),
    path('push', views.iclock_cdata, name='push_no_slash'),
    path('push/', views.iclock_cdata, name='push'),
    # Iclock prefixed paths
    path('iclock/cdata', views.iclock_cdata, name='iclock_cdata_no_slash'),
    path('iclock/cdata/', views.iclock_cdata, name='iclock_cdata'),
    path('iclock/getrequest', views.iclock_getrequest, name='iclock_getrequest_no_slash'),
    path('iclock/getrequest/', views.iclock_getrequest, name='iclock_getrequest'),
    path('iclock/devicecmd', views.iclock_devicecmd, name='iclock_devicecmd_no_slash'),
    path('iclock/devicecmd/', views.iclock_devicecmd, name='iclock_devicecmd'),
    path('iclock/registry', views.iclock_registry, name='iclock_registry_no_slash'),
    path('iclock/registry/', views.iclock_registry, name='iclock_registry'),
    path('iclock/fdata', views.iclock_cdata, name='iclock_fdata_no_slash'),
    path('iclock/fdata/', views.iclock_cdata, name='iclock_fdata'),
    path('iclock/querydata', views.iclock_querydata, name='iclock_querydata_no_slash') if hasattr(views, 'iclock_querydata') else path('iclock/querydata', views.iclock_cdata, name='iclock_querydata_no_slash'),
    path('iclock/querydata/', views.iclock_cdata, name='iclock_querydata'),
    path('iclock/push', views.iclock_cdata, name='iclock_push_no_slash'),
    path('iclock/push/', views.iclock_cdata, name='iclock_push'),
]

from django.urls import re_path
urlpatterns += [
    re_path(r'^https?:/.*', views.iclock_cdata, name='catch_protocol_prefix'),
]

