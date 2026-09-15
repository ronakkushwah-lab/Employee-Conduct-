from django.urls import path, include

from . import views
from .views import *

urlpatterns = [
    path('salary/<int:company_id>/<int:company_staff_id>', SalaryView.as_view(), name='salary'),
    # path('salary/<int:company_id>/<int:company_staff_id>/<int:pk>/', SalaryDetailView.as_view(), name='salary-detail'),
    path('salary/<int:company_id>/<int:company_staff_id>/<int:id>/', SalaryDetailView, name='salary-detail'),
    # path('salary_remove/<int:id>/', views.SalaryRemove.as_view(), name='salary_remove'),
    path('salary_remove/<int:company_id>/<int:company_staff_id>/<int:id>', views.SalaryRemove.as_view(), name='salary_remove'),

    path('salary_update/', Update_salary_View.as_view(), name='salary_update'),
    path('salary-download/<int:company_id>/<int:company_staff_id>/<int:id>', views.GeneratePdf.as_view(), name='salary-download'),

    path('salary-create/', views.CreateSalaryView.as_view(), name='salary-create'),

    # path('genrateSlartyslip/<int:pk>*<str:month>*<str:year>', views.GenrateSalarySlip.as_view(), name="genratesalary"),


]
