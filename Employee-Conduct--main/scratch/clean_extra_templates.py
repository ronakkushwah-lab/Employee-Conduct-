import os

BASE_DIR = r"c:\Users\Dell\Desktop\Ronak Ec\Employee-Conduct--main"

extra_files = [
    os.path.join(BASE_DIR, "managers", "templates", "managers", "basic_layout.html"),
    os.path.join(BASE_DIR, "administration", "templates", "administration", "basic_layout.html"),
    os.path.join(BASE_DIR, "administration", "templates", "administration", "employee-salary.html"),
    os.path.join(BASE_DIR, "administration", "templates", "administration", "manager-salary.html"),
    os.path.join(BASE_DIR, "administration", "templates", "administration", "manager-payslip.html"),
    os.path.join(BASE_DIR, "administration", "templates", "administration", "employee-payslip.html"),
    os.path.join(BASE_DIR, "administration", "templates", "administration", "all-manager-list.html"),
    os.path.join(BASE_DIR, "employee", "templates", "employee", "basic_layout.html"),
]

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

for path in extra_files:
    if not os.path.exists(path):
        continue
    with open(path, "r", encoding="utf-8") as f:
        content = f.read()

    modified = False
    if start_block in content:
        content = content.replace(start_block, """      <div class="sidebar_wrapper">""")
        modified = True

    if end_block in content:
        content = content.replace(end_block, """      </div>""")
        modified = True
    else:
        # Check if there's any other variation of simplebar closing tags
        # E.g. height: 120px or similar
        import re
        content = re.sub(
            r'</div>\s*</div>\s*</div>\s*</div>\s*<div class="simplebar-placeholder"[^>]*></div>\s*</div>\s*<div class="simplebar-track simplebar-horizontal"[^>]*>[\s\S]*?</div>\s*</div>\s*<div class="simplebar-track simplebar-vertical"[^>]*>[\s\S]*?</div>\s*</div>\s*</div>',
            '</div>',
            content
        )
        modified = True

    with open(path, "w", encoding="utf-8") as f:
        f.write(content)
    print(f"Processed {path}")

print("Extra templates processed.")
