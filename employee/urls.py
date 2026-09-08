from django.urls import path
from . import views

urlpatterns = [

    path('employee_profile/<int:company_id>/<int:company_staff_id>', views.employee_profile_view, name='employee_profile'),
    path('upload_profile_image/<int:company_id>/<int:company_staff_id>', views.upload_profile_image, name='upload_profile_image'),
    path('remove_profile_image/<int:company_id>/<int:company_staff_id>', views.remove_profile_image, name='remove_profile_image'),
    path('employee_dashboard/<int:company_id>/<int:company_staff_id>/', views.EmployeeDashboardView, name='employee_dashboard'),


    path('leave/<int:company_id>/<int:company_staff_id>', views.create_leave, name='leave'),
    path('leaves/view/table/<int:company_id>/<int:company_staff_id>', views.view_my_leave_table, name='staffleavetable'),
    path('leave_remove/<int:company_id>/<int:company_staff_id>/<int:id>', views.LeaveRemove.as_view(), name='leave_remove'),
    # path('leavebalance', views.BalanceLeaveView.as_view(), name='leavebalance'),
    path('leavebalance/<int:company_id>/<int:company_staff_id>', views.BalanceLeaveView, name='leavebalance'),


    path('attendance/<int:company_id>/<int:company_staff_id>/', views.attendance, name='attendance'),
    path('attendance_post/<int:company_id>/<int:company_staff_id>', views.attendance_post, name='attendance_post'),
    path('attendance_grid_Data/<int:company_id>/<int:company_staff_id>', views.attendance_grid_data, name='attendance_grid_Data'),

    # path('task/list/<int:company_id>/<int:company_staff_id>', views.TaskListView.as_view(), name= 'task-list'),
    path('task/list/<int:company_id>/<int:company_staff_id>', views.taskList, name= 'task-list'),
    # path('salary/list/<int:company_id>/<int:company_staff_id>', views.SalaryListView.as_view(), name='salary-list'),
    path('salary/list/<int:company_id>/<int:company_staff_id>', views.SalaryListView, name='salary-list'),



    path('notification/<int:company_id>/<int:company_staff_id>', views.notifications, name='notification'),
    path(
        'notification_delete/<int:company_id>/<int:company_staff_id>/<int:notification_id>/',
        views.delete_employee_notification,
        name='notification_delete',
    ),

    path('resign/<int:company_id>/<int:company_staff_id>', views.create_resign, name='resign'),
    path('resign/view/table/<int:company_id>/<int:company_staff_id>', views.view_my_resign_table, name='staffresigntable'),
    path('resign_remove/<int:company_id>/<int:company_staff_id>/<int:id>', views.ResignRemove.as_view(), name='resign_remove'),
    path('holiday_list/<int:company_id>/<int:company_staff_id>', views.holidays, name='holiday_list'),

    path('all_document_Views/<int:company_id>/<int:company_staff_id>', views.All_document_View, name='all_document_Views'),

    path('regularization/<int:company_id>/<int:company_staff_id>', views.create_regularization, name='regularization'),
    # path('regularization/view/table/', views.view_my_regularization_table.as_view(), name='staffregularizationtable'),
    path('regularization/view/table/<int:company_id>/<int:company_staff_id>', views.regularization_table, name='staffregularizationtable'),

    path('entries-create/', views.EntriesCreateView, name='entries-create'),
    path('entries-detail/<int:company_id>/<int:company_staff_id>', views.EntryDetailView, name='entries-detail'),
    path('entry_remove/<int:company_id>/<int:company_staff_id>/<id>', views.EntryRemove.as_view(), name='entry_remove'),

    path('regularization_required/<int:company_id>/<int:company_staff_id>', views.regularization_required_attendance, name='regularization_required'),


    path('create_entry/<int:company_id>/<int:company_staff_id>', views.create_entry, name='create_entry'),

    path('create_leave/<int:company_id>/<int:company_staff_id>', views.create_leave, name='create_leave'),
    path('create_resign/<int:company_id>/<int:company_staff_id>', views.create_resign, name='create_resign'),
    path('create_regularization/<int:company_id>/<int:company_staff_id>', views.create_regularization, name='create_regularization'),

    path('create_ducuments/<int:company_id>/<int:company_staff_id>', views.create_ducuments, name='create_ducuments'),
    path('change_password/<int:company_id>/<int:company_staff_id>', views.ChangePassword, name='change_password'),
    path("mynotifications/<int:company_id>/<int:company_staff_id>/", views.MyNotification, name="mynotifications"),



]
