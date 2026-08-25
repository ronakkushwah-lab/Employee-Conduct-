from . import search
from django.urls import path
from . import views
from .views import All_entry





urlpatterns = [
    path('index/<int:company_id>/<int:company_staff_id>/', views.IndexView, name='index'),
# ------------------------------Employees-----------------------------------------------------------------------#
    path('update_employe/<int:company_id>/<int:id>',views.Update_Employees_View,name='update_employe' ),

    path('Remove_employee/<int:company_id>/<int:company_staff_id>/<int:id>',views.Remove_Employee,name='Remove_employee'),
    path('Remove_Employee_List/<int:id>',views.Remove_Employee_List,name='Remove_Employee_List'),

    path('all_employee/<int:company_id>/<int:company_staff_id>',views.All_Employee_View,name='all_employee'),
    path('all_employee_list/',views.All_Employee_List_View,name='all_employee_list' ),
    path('edit_employee/<int:company_id>/<int:company_staff_id>', views.Employee_Edit_View, name='edit_employee'),
    path('registeremployee/<int:company_id>/<int:company_staff_id>',views.Register_Employee_View,name='registeremployee' ),

    path(f'all_manager/<int:company_id>/<int:company_staff_id>', views.All_manager_View, name='all_manager'),
    path('all_manager_list/', views.All_manager_List_View, name='all_manager_list'),
    path('registermanager/<int:company_id>/<int:company_staff_id>', views.Register_manager_View, name='registermanager'),

    path('update_manager/<int:id>', views.Update_manager_View, name='update_manager'),

    path('Remove_manager/<int:company_id>/<int:company_staff_id>/<int:id>', views.Remove_manager, name='Remove_manager'),
    path('Remove_manager_List/<int:company_id>/<int:company_staff_id>/<int:id>', views.Remove_manager_List, name='Remove_manager_List'),
    path('edit_manager/<int:company_id>/<int:company_staff_id>', views.manager_Edit_View, name='edit_manager'),

    



# ------------------------------client---------------------------------------------------------------------------
#     path('clients/<int:company_id>/<int:company_staff_id>',views.CreateClientsView.as_view(),name='clients' ),
    path('clients/<int:company_id>/<int:company_staff_id>', views.CreateClientsView, name='clients'),
    path('editClient/<int:company_id>/<int:company_staff_id>',views.EditClient,name='editClient'),
    path('clients_list/',views.CreateClientsListView.as_view(),name='clients_list' ),
    path('clients_grid/<int:company_id>/<int:company_staff_id>',views.CreateClientsGridView.as_view(),name='clients_grid' ),
    path('clients_remove/<int:company_id>/<int:company_staff_id>/<int:id>',views.ClientRemove.as_view(),name='clients_remove' ),
    path('clients_remove_grid/<int:id>',views.ClientRemoveGrid.as_view(),name='clients_remove_grid' ),
    path('client_manage_grid/<int:pk>',views.ClientManageGrid.as_view(),name='client_manage_grid' ),
    path('client_manage_list/<int:pk>',views.ClientManageList.as_view(),name='client_manage_list' ),
    path('client_list/<int:company_id>/<int:company_staff_id>',views.All_client_View,name='client_list'),

# ------------------------------client--------------------------------------------------------------------------

# ------------------------------Lead---------------------------------------------------------------------------
#     path('leads/',views.CreateLeadView.as_view(),name='leads' ),
    path('leads/<int:company_id>/<int:company_staff_id>', views.CreateLeadView, name='leads'),
    path('leads_list/<int:company_id>/<int:company_staff_id>',views.All_lead_View,name='leads_list'),
    path('leads_remove/<int:company_id>/<int:company_staff_id>/<int:id>',views.LeadsRemove.as_view(),name='leads_remove' ),
    path('leads_manage/<int:pk>',views.LeadManage.as_view(),name='leads_manage' ),
    path('edit_lead/<int:company_id>/<int:company_staff_id>', views.lead_Edit_View, name='edit_lead'),
# ------------------------------/Lead--------------------------------------------------------------------------


    path('employee_search/<int:company_id>/<int:company_staff_id>',search.EmployeeSearchResultsView,name='employee_search'),
    path('manager_search/', search.managerSearchResultsView, name='manager_search'),
    path('changepasswordView/', views.ChangePassword, name='changepasswordView'),

    path('entry/detail/<int:pk>', views.EntryDetailView.as_view(),
         name='entry-detail'
         ),
    path('All_entry/<int:company_id>/<int:company_staff_id>', All_entry, name='All_entry'),
    path('remove_entry/<int:id>', views.EntryRemove.as_view(), name='remove_entry'),

    # path('task/new', views.TaskCreateView.as_view(), name='task-create'),
    path('task/new/<int:company_id>/<int:company_staff_id>', views.TaskCreateView, name='task-create'),
    path('task/<int:pk>', views.TaskDetailView.as_view(), name='task-detail'),
    path('task/<int:pk>/delete', views.TaskDeleteView.as_view(), name='task-delete'),
    path('projectlist/<int:company_id>/<int:company_staff_id>', views.Project_list, name='projectlist'),
    path('project_remove/<int:company_id>/<int:company_staff_id>/<id>', views.ProjectRemove.as_view(), name='project_remove'),

    path('manager-project/new/<int:company_id>/<int:company_staff_id>', views.ManagerProjectCreateView, name='manager-project-create'),
    path('manager-project/list/<int:company_id>/<int:company_staff_id>', views.ManagerProject_list, name='managerprojectlist'),
    path('manager-project/remove/<int:company_id>/<int:company_staff_id>/<id>', views.ManagerProjectRemove.as_view(), name='managerproject_remove'),

    path('leaves/pending/all/<int:company_id>/<int:company_staff_id>', views.leaves_list, name='leaveslist'),
    path('leaves/approved/all/<int:company_id>/<int:company_staff_id>', views.leaves_approved_list, name='approvedleaveslist'),
    path('leaves/cancel/all/', views.cancel_leaves_list, name='canceleaveslist'),
    path('leave/approve/<int:company_id>/<int:company_staff_id>/<int:id>/', views.approve_leave, name='userleaveapprove'),
    path('leave/unapprove/<int:id>/', views.unapprove_leave, name='userleaveunapprove'),
    path('leave/cancel/<int:id>/', views.cancel_leave, name='userleavecancel'),
    path('leave/uncancel/<int:id>/', views.uncancel_leave, name='userleaveuncancel'),
    path('leaves/rejected/all/<int:company_id>/<int:company_staff_id>', views.leave_rejected_list, name='leavesrejected'),
    path('leave/reject/<int:company_id>/<int:company_staff_id>/<int:id>/', views.reject_leave, name='rejected'),
    path('leave/unreject/<int:id>/', views.unreject_leave, name='unreject'),
    path('leaves/all/view/<int:id>/', views.leaves_view, name='userleaveview'),

    path('add-leaves-balance/<int:company_id>/<int:company_staff_id>/', views.add_leaves_balance, name='add_leaves_balance'),
    path('mbalance-leave/<int:company_id>/<int:company_staff_id>/', views.add_leaves_balance, name='mbalance-leave'),
    path('balancelist/<int:company_id>/<int:company_staff_id>/', views.Balance_list, name='balancelist'),
    path('balance_remove/<int:company_id>/<int:company_staff_id>/<id>', views.BalanceRemove.as_view(), name='balance_remove'),

    path('notifications/<int:company_id>/<int:company_staff_id>', views.notifications,name='notifications'),
    path('createnotifications/<int:company_id>/<int:company_staff_id>', views.createnotifications, name='createnotifications'),
    path('getnotification/', views.getnotification, name='getnotification'),
    path('notification_edit/<int:company_id>/<int:company_staff_id>', views.Notification_Edit_View, name='notification_edit'),
    path('notification_remove/<int:company_id>/<int:company_staff_id>/<int:id>', views.NotificationRemove.as_view(), name='department_remove'),

    path('attendancee/<int:company_id>/<int:company_staff_id>', views.attendance, name='attendancee'),
    path('getattendance/', views.getattendance, name='getattendance'),
    path('attendance_remove/<int:company_id>/<int:company_staff_id>/<id>', views.AttendanceRemove.as_view(), name='attendance_remove'),
    path('attendance_manage/<int:pk>', views.AttendanceManage.as_view(), name='attendance_manage'),
    path('edit_attendance/<int:company_id>/<int:company_staff_id>', views.attendance_Edit_View, name='edit_attendance'),
    path('biometric-machines/<int:company_id>/<int:company_staff_id>', views.biometric_machines, name='biometric_machines'),

    path('resign/pending/all/<int:company_id>/<int:company_staff_id>', views.resign_list, name='resignlist'),
    path('resign/approved/all/<int:company_id>/<int:company_staff_id>', views.resign_approved_list, name='approvedresignlist'),
    path('resign/cancel/all/<int:company_id>/<int:company_staff_id>', views.cancel_resign_list, name='cancelresignlist'),
    path('resign/approve/<int:company_id>/<int:company_staff_id>/<int:id>/', views.approve_resign, name='userresignapprove'),
    path('resign/unapprove/<int:id>/', views.unapprove_resign, name='userresignunapprove'),
    path('resign/cancel/<int:company_id>/<int:company_staff_id>/<int:id>/', views.cancel_resign, name='userresigncancel'),
    path('resign/uncancel/<int:id>/', views.uncancel_resign, name='userresignuncancel'),
    path('resign/rejected/all/<int:company_id>/<int:company_staff_id>', views.resign_rejected_list, name='resignrejected'),
    path('resign/reject/<int:company_id>/<int:company_staff_id>/<int:id>/', views.reject_resign, name='reject'),
    path('resign/unreject/<int:company_id>/<int:company_staff_id>/<int:id>/', views.unreject_resign, name='unreject'),
    path('resign/all/view/<int:id>/', views.resign_view, name='userresignview'),

    path('holidays/<int:company_id>/<int:company_staff_id>', views.holidays,  name='holidays'),
    path('fnholidays/', views.fnholidays, name='fnholiday'),
    path('getdatas/', views.getdatas, name='getdata'),
    path('delholiday/<int:company_id>/<int:company_staff_id>/<int:id>/', views.delholiday.as_view(), name='delholiday'),
    path('holidaylist/<int:company_id>/<int:company_staff_id>', views.holiday_list, name='holidaylist'),

    path('alldocument/<int:company_id>/<int:company_staff_id>', views.PostListView.as_view(), name='alldocument'),
    path('user/', views.UserPostListView.as_view(), name='user-posts'),
    # path('allpost/<int:company_id>/<int:company_staff_id>/<int:id>/', views.PostDetailView.as_view(), name='allpost'),
    path('allpost/<int:company_id>/<int:company_staff_id>/<int:id>/', views.PostDetailView, name='allpost'),
    path('search/', views.search, name='search'),
    path('all_document_View/<int:company_id>/<int:company_staff_id>', views.All_document_View, name='all_document_View'),
    path('post-delete/<int:company_id>/<int:company_staff_id>/<int:id>', views.PostDeleteView.as_view(), name='post-delete'),



    # path('department/', views.DepartmentCreateView.as_view(), name='department'),
    path('department/<int:company_id>/<int:company_staff_id>', views.DepartmentCreateView, name='department'),
    # path('department_lst/', views.DepartmentList.as_view(), name='department_lst'),
    path('department_remove/<int:company_id>/<int:company_staff_id>/<int:id>', views.DepartmentRemove.as_view(), name='department_remove'),
    path('department_manage/<int:pk>', views.ManageDepartment.as_view(), name='department_manage'),
    path('department_edit/<int:company_id>/<int:company_staff_id>', views.department_Edit_View, name='department_edit'),
    path('department_lst/<int:company_id>/<int:company_staff_id>', views.DepartmentList, name='department_lst'),

    path('regularization/pending/all/<int:company_id>/<int:company_staff_id>', views.regularization_list, name='regularizationlist'),
    path('regularization/approved/all/<int:company_id>/<int:company_staff_id>', views.regularization_approved_list, name='approvedregularizationlist'),
    path('regularization/cancel/all/<int:company_id>/<int:company_staff_id>', views.cancel_regularization_list, name='cancelregularizationlist'),
    path('regularization/approve/<int:company_id>/<int:company_staff_id>/<int:id>', views.approve_regularization, name='userregularizationapprove'),
    path('regularization/unapprove/<int:id>/', views.unapprove_regularization, name='userregularizationunapprove'),
    path('regularization/cancel/<int:company_id>/<int:company_staff_id>/<int:id>/', views.cancel_regularization, name='userregularizationcancel'),
    path('regularization/uncancel/<int:id>/', views.uncancel_regularization, name='userregularizationuncancel'),
    path('regularization/rejected/all/', views.regularization_rejected_list, name='regularizationrejected'),
    path('regularization/reject/<int:id>/', views.reject_regularization, name='reject'),
    path('regularization/unreject/<int:id>/', views.unreject_leave, name='unreject'),
    # path('regularization/all/view/<int:id>/', views.regularization_view, name='userregularizationview'),

    path('attendancesearch/<int:company_id>/<int:company_staff_id>', views.Attendancesearch, name='attendancesearch'),

    path('mregularization/pending/all/<int:company_id>/<int:company_staff_id>', views.mregularization_list, name='mregularizationlist'),
    path('mregularization/approved/all/<int:company_id>/<int:company_staff_id>', views.mregularization_approved_list, name='mapprovedregularizationlist'),
    path('mregularization/cancel/all/<int:company_id>/<int:company_staff_id>', views.mcancel_regularization_list, name='mcancelregularizationlist'),
    path('mregularization/approve/<int:company_id>/<int:company_staff_id>/<int:id>/', views.mapprove_regularization, name='muserregularizationapprove'),
    path('mregularization/unapprove/<int:id>/', views.munapprove_regularization, name='muserregularizationunapprove'),
    path('mregularization/cancel/<int:company_id>/<int:company_staff_id>/<int:id>/', views.mcancel_regularization, name='muserregularizationcancel'),
    path('mregularization/uncancel/<int:id>/', views.muncancel_regularization, name='muserregularizationuncancel'),
    path('mregularization/rejected/all/', views.mregularization_rejected_list, name='mregularizationrejected'),
    path('mregularization/reject/<int:id>/', views.mreject_regularization, name='mreject'),
    path('mregularization/all/view/<int:id>/', views.mregularization_view, name='muserregularizationview'),

    path('mattendancee/<int:company_id>/<int:company_staff_id>', views.mattendance, name='mattendancee'),
    # path('mgetattendance/', views.mgetattendance, name='mgetattendance'),
    path('mattendance_remove/<int:company_id>/<int:company_staff_id>/<id>', views.mAttendanceRemove.as_view(), name='mattendance_remove'),
    path('mattendance_manage/<int:pk>', views.mAttendanceManage.as_view(), name='mattendance_manage'),
    path('mattendancesearch/<int:company_id>/<int:company_staff_id>', views.mAttendancesearch, name='mattendancesearch'),
    path('mattendanceEdit/<int:company_id>/<int:company_staff_id>', views.Mattendance_Edit_View, name='mattendanceEdit'),

    # path('assign/new', views.assignCreateView.as_view(), name='assign-create'),
    path('assign/new/<int:company_id>/<int:company_staff_id>', views.assignCreateView, name='assign-create'),
    path('assign/<int:pk>', views.assignDetailView.as_view(), name='assign-detail'),
    path('assign/<int:pk>/delete', views.aasignDeleteView.as_view(), name='assign-delete'),
    path('assignlist/<int:company_id>/<int:company_staff_id>', views.Assign_list, name='assignlist'),
    path('assign_remove/<int:company_id>/<int:company_staff_id>/<id>', views.AssignRemove.as_view(), name='assign_remove'),

    path('manager_document_View/<int:company_id>/<int:company_staff_id>', views.All_document_Views, name='manager_document_View'),

    path('malldocument', views.ManagerPostListView.as_view(), name='malldocument'),
    path('muser/', views.MPostListView.as_view(), name='muser-posts'),
    path('mallpost/<int:company_id>/<int:company_staff_id>/<int:id>/', views.MPostDetailView, name='mallpost'),
    path('search/', views.search, name='search'),
    path('mpost-delete/<int:company_id>/<int:company_staff_id>/<int:id>', views.MPostDeleteView.as_view(), name='mpost-delete'),

    path("sendemail/<int:company_id>/<int:company_staff_id>/", views.sendemail, name="sendemail"),

    path("managernotification/<int:company_id>/<int:company_staff_id>/", views.Managernotifications, name="managernotification"),
    path("P_name/", views.P_name, name="P_name"),

    # Email Notifications URLs
    path('email_notifications/<int:company_id>/<int:company_staff_id>', views.email_notifications, name='email_notifications'),
    path('fetch_emails/<int:company_id>/<int:company_staff_id>', views.fetch_emails_view, name='fetch_emails'),
    path('mark_email_read/<int:company_id>/<int:company_staff_id>/<int:id>', views.mark_email_read, name='mark_email_read'),
    path('mark_all_emails_read/<int:company_id>/<int:company_staff_id>', views.mark_all_emails_read, name='mark_all_emails_read'),
    path('email_notification_detail/<int:company_id>/<int:company_staff_id>/<int:id>', views.email_notification_detail, name='email_notification_detail'),
    path('delete_email_notification/<int:company_id>/<int:company_staff_id>/<int:id>', views.delete_email_notification, name='delete_email_notification'),
    
    # Employee Documents URLs
    path(
        "all-documents/<int:company_id>/<int:company_staff_id>/",
        views.all_documents,
        name="all_documents"
    ),
    path(
        "delete-employee-documents/<int:company_id>/<int:company_staff_id>/<int:id>/",
        views.delete_employee_documents,
        name="delete_employee_documents"
    ),

    # Master Bulk Import & Templates (Excel / CSV)
    path('bulk_import_staff/<int:company_id>/<int:company_staff_id>/', views.bulk_import_staff, name='bulk_import_staff'),
    path('download_master_template/', views.download_master_template, name='download_master_template'),
    path('bulk_import_attendance/<int:company_id>/<int:company_staff_id>/', views.bulk_import_attendance, name='bulk_import_attendance'),
    path('download_attendance_template/', views.download_attendance_template, name='download_attendance_template'),
]
