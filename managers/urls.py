from django.urls import path
from . import views
# from employee import views as em_views


urlpatterns = [

    path('dashboard/<int:company_id>/<int:company_staff_id>/', views.ManagerDashboardView, name='dashboard'),
    path('update_manger/<int:id>', views.managerUpdateView.as_view(), name='update_manger'),
    path('manager_profile/<int:company_id>/<int:company_staff_id>', views.manager_profile_view, name='manager_profile'),
    path('upload_manager_profile_image/<int:company_id>/<int:company_staff_id>', views.upload_manager_profile_image, name='upload_manager_profile_image'),
    path('remove_manager_profile_image/<int:company_id>/<int:company_staff_id>', views.remove_manager_profile_image, name='remove_manager_profile_image'),

    path('mleave/<int:company_id>/<int:company_staff_id>', views.leave_creation, name='mleave'),
    path('mleaves/view/table/<int:company_id>/<int:company_staff_id>', views.view_my_leave_table, name='mstaffleavetable'),
    path('mleave_remove/<id>', views.LeaveRemove.as_view(), name='mleave_remove'),
    path('mleavebalance/<int:company_id>/<int:company_staff_id>', views.BalanceLeaveView, name='mleavebalance'),

    path('attendanc/<int:company_id>/<int:company_staff_id>', views.attendance, name='attendanc'),
    path('attendanc_post/<int:company_id>/<int:company_staff_id>', views.attendance_post, name='attendanc_post'),
    path('attendanc_grid_Data/<int:company_id>/<int:company_staff_id>', views.attendance_grid_data, name='attendanc_grid_Data'),

    path('mtask/list/<int:company_id>/<int:company_staff_id>', views.TaskListView, name='mtask-list'),
    path('salary/lists/<int:company_id>/<int:company_staff_id>/', views.SalaryListView, name='salary-lists'),

    path('mnotification/<int:company_id>/<int:company_staff_id>', views.notifications, name='mnotification'),

    path('mresign/', views.resign_creation, name='mresign'),
    path('mresign/view/table/<int:company_id>/<int:company_staff_id>', views.view_my_resign_table, name='mstaffresigntable'),
    path('mresign_remove/<id>', views.ResignRemove.as_view(), name='mresign_remove'),
    path('mholiday_list/<int:company_id>/<int:company_staff_id>', views.holidays, name='mholiday_list'),

    path('', views.PostListView.as_view(), name='blog-home'),
    path('mpost_list/', views.UserPostListView.as_view(), name='mpost_list'),
    path('mpost/<int:pk>/', views.PostDetailView.as_view(), name='mpost-detail'),
    path('mpost/new/', views.PostCreateView.as_view(), name='mpost-create'),
    path('mpost/<int:pk>/update/', views.PostUpdateView.as_view(), name='mpost-update'),
    path('mpost/<int:pk>/delete/', views.PostDeleteView.as_view(), name='mpost-delete'),
    path('media/Files/<int:pk>', views.PostDeleteView.as_view(), name='mpost-delete'),

    path('mregularizations/', views.mregularization, name='mregularizations'),
    path('mregularizations/view/table/<int:company_id>/<int:company_staff_id>', views.view_my_regularization_table, name='mstaffregularizationstable'),

    path('mtask/new', views.TaskCreateViews.as_view(), name='mtask-create'),
    path('mtask/<int:pk>', views.TaskDetailViews.as_view(), name='mtask-detail'),
    path('mtask/<int:pk>/delete', views.TaskDeleteViews.as_view(), name='mtask-delete'),
    path('mprojectlist/<int:company_id>/<int:company_staff_id>', views.Project_list, name='mprojectlist'),
    path('mproject_remove/<int:company_id>/<int:company_staff_id>/<id>', views.ProjectRemove.as_view(), name='mproject_remove'),

    path('mnattendancee/<int:company_id>/<int:company_staff_id>', views.attendanc, name='mnattendancee'),
    path('mnattendancee_edit/<int:company_id>/<int:company_staff_id>', views.mattendance_Edit_View, name='mnattendancee_edit'),
    path('mnattendance_remove/<int:company_id>/<int:company_staff_id>/<id>', views.AttendanceRemove.as_view(), name='mnattendance_remove'),
    path('mnattendance_manage/<int:pk>', views.AttendanceManage.as_view(), name='mnattendance_manage'),
    path('mregularization_required/<int:company_id>/<int:company_staff_id>', views.regularization_required_attendance, name='mregularization_required'),

    path('mnregularization/pending/all/<int:company_id>/<int:company_staff_id>', views.regularization_list, name='mnregularizationlist'),
    path('mnregularization/approved/all/<int:company_id>/<int:company_staff_id>', views.regularization_approved_list, name='mnapprovedregularizationlist'),
    path('mnregularization/cancel/all/<int:company_id>/<int:company_staff_id>', views.cancel_regularization_list, name='mncancelregularizationlist'),
    path('mnregularization/approve/<int:company_id>/<int:company_staff_id>/<int:id>/', views.approve_regularization, name='mnuserregularizationapprove'),
    path('mnregularization/unapprove/<int:id>/', views.unapprove_regularization, name='mnuserregularizationunapprove'),
    path('mnregularization/cancel/<int:company_id>/<int:company_staff_id>/<int:id>/', views.cancel_regularization, name='mnuserregularizationcancel'),
    path('mnregularization/uncancel/<int:id>/', views.uncancel_regularization, name='mnuserregularizationuncancel'),
    path('mnregularization/rejected/all/', views.regularization_rejected_list, name='mnregularizationrejected'),
    path('mnregularization/reject/<int:id>/', views.reject_regularization, name='mnreject'),
    path('mnregularization/all/view/<int:id>/', views.regularization_view, name='mnuserregularizationview'),

    path('mnattendancesearch/', views.Attendancesearch, name='mnattendancesearch'),

    path('assign/list/<int:company_id>/<int:company_staff_id>', views.AssignListView, name='assign-list'),

    path('entry-list/<int:company_id>/<int:company_staff_id>', views.EntryListView, name='entry-list'),
    path('entry_remove/<id>', views.EntryRemove.as_view(), name='entry_removes'),


    path('create_ducument/<int:company_id>/<int:company_staff_id>', views.create_ducument, name='create_ducument'),

    path('create_mregularizations/<int:company_id>/<int:company_staff_id>', views.create_mregularizations, name='create_mregularizations'),

    path('add_project/<int:company_id>/<int:company_staff_id>', views.add_project, name='add_project'),

    path('add_leave/<int:company_id>/<int:company_staff_id>', views.add_leave, name='add_leave'),

    path('create_mresignation/<int:company_id>/<int:company_staff_id>', views.create_mresignation, name='create_mresignation'),

    path('resign_list/<int:company_id>/<int:company_staff_id>',views.resign_list, name='resign_list'),

    path('resign/approve/<int:company_id>/<int:company_staff_id>/<int:id>/', views.approve_resign, name='userresignapproved'),
    path('resign/cancel/<int:company_id>/<int:company_staff_id>/<int:id>/', views.cancel_resign, name='userresigncanceled'),

    path('leave_list/<int:company_id>/<int:company_staff_id>', views.leave_list, name='leave_list'),

    path('leaves/approved/all/<int:company_id>/<int:company_staff_id>/', views.leaves_approved_list, name='mapprovedleaveslist'),
    path('leaves/cancel/all/<int:company_id>/<int:company_staff_id>/', views.cancel_leaves_list, name='mcanceleaveslist'),
    path('leave/approve/<int:company_id>/<int:company_staff_id>/<int:id>/', views.approve_leave, name='muserleaveapprove'),
    path('leave/unapprove/<int:id>/', views.unapprove_leave, name='muserleaveunapprove'),
    path('leave/cancel/<int:company_id>/<int:company_staff_id>/<int:id>/', views.cancel_leave, name='muserleavecancel'),
    path('leave/uncancel/<int:id>/', views.uncancel_leave, name='muserleaveuncancel'),
    path('leaves/rejected/all/', views.leave_rejected_list, name='mleavesrejected'),
    path('leave/reject/<int:company_id>/<int:company_staff_id>/<int:id>/', views.reject_leave, name='mrejected'),
    path('leave/unreject/<int:id>/', views.unreject_leave, name='munreject'),
    path('leaves/all/view/<int:id>/', views.leaves_view, name='muserleaveview'),
    path('document_View/<int:company_id>/<int:company_staff_id>', views.All_document_Views, name='document_View'),

    path('employee_searchs/<int:company_id>/<int:company_staff_id>', views.Attendancesearch, name='employee_searchs'),
    path('mchange_password/<int:company_id>/<int:company_staff_id>', views.ChangePassword, name='mchange_password'),
    path("mynotification/<int:company_id>/<int:company_staff_id>/", views.MyNotification, name="mynotification"),
    path(
        "mynotification_delete/<int:company_id>/<int:company_staff_id>/<int:notification_id>/",
        views.delete_manager_notification,
        name="mynotification_delete",
    ),
    path("employeenotification/<int:company_id>/<int:company_staff_id>/", views.Employeenotifications, name="employeenotification"),


]