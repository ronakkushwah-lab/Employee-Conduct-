from django.urls import path

from . import views


app_name = 'biometric'

urlpatterns = [
    path('log/', views.bridge_punch, name='legacy_log'),
    path('attendance/biometric-punch/', views.bridge_punch, name='biometric_punch'),
    path('attendance/manual-punch/', views.manual_punch, name='manual_punch'),
    path('attendance/http-push/', views.http_push, name='http_push'),
    path('attendance/biometric-heartbeat/', views.biometric_heartbeat, name='biometric_heartbeat'),
]
