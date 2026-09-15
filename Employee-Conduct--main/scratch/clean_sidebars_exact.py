import os

BASE_DIR = r"c:\Users\Dell\Desktop\Ronak Ec\Employee-Conduct--main"

# 1. administration/templates/administration/sidebar.html
admin_path = os.path.join(BASE_DIR, "administration", "templates", "administration", "sidebar.html")
with open(admin_path, "r", encoding="utf-8") as f:
    admin_content = f.read()

start_block = """      <div class="sidebar_wrapper" data-simplebar>
        <div class="simplebar-wrapper">
          <div class="simplebar-height-auto-observer-wrapper">
            <div class="simplebar-height-auto-observer"></div>
          </div>
          <div class="simplebar-mask">
            <div class="simplebar-offset">
              <div class="simplebar-content-wrapper">
                <div class="simplebar-content mm-active">"""

end_block = """                </div>
              </div>
            </div>
          </div>
          <div class="simplebar-placeholder"></div>
        </div>
        <div class="simplebar-track simplebar-horizontal" style="visibility: hidden;">
          <div class="simplebar-scrollbar" style="width: 0px; display: none;"></div>
        </div>
        <div class="simplebar-track simplebar-vertical" style="visibility: visible;">
          <div class="simplebar-scrollbar" style="height: 83px; transform: translate3d(0px, 0px, 0px); display: block;"></div>
        </div>
      </div>"""

if start_block in admin_content and end_block in admin_content:
    admin_content = admin_content.replace(start_block, """      <div class="sidebar_wrapper">""")
    admin_content = admin_content.replace(end_block, """      </div>""")
    with open(admin_path, "w", encoding="utf-8") as f:
        f.write(admin_content)
    print("Admin sidebar cleaned successfully.")
else:
    print("Admin sidebar blocks not matched exactly!")

# 2. employee/templates/employee/sidebar.html
emp_path = os.path.join(BASE_DIR, "employee", "templates", "employee", "sidebar.html")
with open(emp_path, "r", encoding="utf-8") as f:
    emp_content = f.read()

if start_block in emp_content and end_block in emp_content:
    emp_content = emp_content.replace(start_block, """      <div class="sidebar_wrapper">""")
    emp_content = emp_content.replace(end_block, """      </div>""")
    with open(emp_path, "w", encoding="utf-8") as f:
        f.write(emp_content)
    print("Employee sidebar cleaned successfully.")
else:
    print("Employee sidebar blocks not matched exactly!")

# 3. managers/templates/managers/sidebar.html
mgr_path = os.path.join(BASE_DIR, "managers", "templates", "managers", "sidebar.html")
with open(mgr_path, "r", encoding="utf-8") as f:
    mgr_content = f.read()

if start_block in mgr_content:
    mgr_content = mgr_content.replace(start_block, """      <div class="sidebar_wrapper">""")
    # Check end block for manager sidebar
    mgr_content = mgr_content.replace(end_block, """      </div>""")
    with open(mgr_path, "w", encoding="utf-8") as f:
        f.write(mgr_content)
    print("Manager sidebar cleaned successfully.")
else:
    # Try normalized spacing
    print("Manager start block not matched.")
