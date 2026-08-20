from django.urls import path, include

from . import views
from .views import *

urlpatterns = [
        # path('balance-leave', views.BalanceCreateView.as_view(), name='balance-leave'),
        path('balance-leave/<int:company_id>/<int:company_staff_id>', views.BalanceCreateView, name='balance-leave'),
        path('balance/<int:pk>', views.BalanceDetailView.as_view(), name='balance-detail'),
        path('balancelists/<int:company_id>/<int:company_staff_id>', views.Balance_list, name='balancelists'),
        path('balance_removes/<int:company_id>/<int:company_staff_id>/<id>', views.BalanceRemove.as_view(), name='balance_removes'),

]
