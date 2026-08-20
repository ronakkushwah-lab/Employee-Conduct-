from django.urls import path, include

from . import views
from .views import *

urlpatterns = [
        path('mbalance-leave/<int:company_id>/<int:company_staff_id>', views.BalanceCreateView, name='mbalance-leave'),
        path('mbalance/<int:pk>', views.BalanceDetailView.as_view(), name='mbalance-detail'),
        path('mbalance/<int:pk>/delete', views.BalanceDeleteView.as_view(), name='mbalance-delete'),

]
