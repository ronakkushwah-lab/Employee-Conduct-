from django.urls import path, include
from .views import SalaryView, SalaryDetailView


from . import views
from .views import *

urlpatterns = [
    path('msalary/<int:company_id>/<int:company_staff_id>', SalaryView.as_view(), name='msalary'),
    path('msalary-detail/<int:company_id>/<int:company_staff_id>/<int:id>/', SalaryDetailView, name='msalary-detail'),
    # path('salary_remove/<int:id>/', views.SalaryRemove.as_view(), name='salary_remove'),
    path('msalary_remove/<int:company_id>/<int:company_staff_id>/<int:id>', views.SalaryRemove.as_view(), name='msalary_remove'),

    path('msalary_update/', Update_salary_View.as_view(), name='msalary_update'),
    path('msalary-download/<int:company_id>/<int:company_staff_id>/<id>', views.GeneratePdf.as_view(), name='msalary-download'),
    
    # path('genrateSlartyslip/<int:pk>*<str:month>*<str:year>', views.GenrateSalarySlip.as_view(), name="genratesalary"),


]

