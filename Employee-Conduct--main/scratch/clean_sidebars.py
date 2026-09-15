import os
import re

BASE_DIR = r"c:\Users\Dell\Desktop\Ronak Ec\Employee-Conduct--main"

sidebar_files = [
    os.path.join(BASE_DIR, "administration", "templates", "administration", "sidebar.html"),
    os.path.join(BASE_DIR, "employee", "templates", "employee", "sidebar.html"),
    os.path.join(BASE_DIR, "managers", "templates", "managers", "sidebar.html"),
]

for path in sidebar_files:
    with open(path, "r", encoding="utf-8") as f:
        content = f.read()

    # Replace start of sidebar:
    # <div class="sidebar_wrapper" data-simplebar>
    #   <div class="simplebar-wrapper">
    #     <div class="simplebar-height-auto-observer-wrapper">
    #       <div class="simplebar-height-auto-observer"></div>
    #     </div>
    #     <div class="simplebar-mask">
    #       <div class="simplebar-offset">
    #         <div class="simplebar-content-wrapper">
    #           <div class="simplebar-content mm-active">
    #             <div class="sidebar-header">
    old_start_pattern = re.compile(
        r'<div class="sidebar_wrapper"\s*data-simplebar>\s*<div class="simplebar-wrapper">[\s\S]*?<div class="sidebar-header">',
        re.MULTILINE
    )
    content = old_start_pattern.sub('<div class="sidebar_wrapper">\n        <div class="sidebar-header">', content)

    # Replace end of sidebar:
    #                 </div>
    #               </div>
    #             </div>
    #           </div>
    #           <div class="simplebar-placeholder"></div>
    #         </div>
    #         <div class="simplebar-track simplebar-horizontal" ...>...</div>
    #         <div class="simplebar-track simplebar-vertical" ...>...</div>
    #       </div>
    old_end_pattern = re.compile(
        r'</div>\s*</div>\s*</div>\s*</div>\s*<div class="simplebar-placeholder"[\s\S]*?</div>\s*</div>\s*</div>\s*</div>',
        re.MULTILINE
    )
    # Also handle alternate variations:
    old_end_pattern2 = re.compile(
        r'</ul>\s*</div>\s*</div>\s*</div>\s*</div>\s*<div class="simplebar-placeholder"[\s\S]*?</div>\s*</div>\s*</div>\s*</div>',
        re.MULTILINE
    )
    old_end_pattern3 = re.compile(
        r'</ul>\s*</div>\s*</div>\s*</div>\s*</div>\s*<div class="simplebar-placeholder"[\s\S]*?</div>\s*</div>\s*</div>\s*</div>\s*</div>',
        re.MULTILINE
    )
    old_end_pattern4 = re.compile(
        r'</ul>[\s\S]*?<div class="simplebar-placeholder"[\s\S]*?<div class="simplebar-track simplebar-vertical"[\s\S]*?</div>\s*</div>\s*</div>',
        re.MULTILINE
    )

    if re.search(r'simplebar-placeholder', content):
        content = re.sub(
            r'</ul>[\s\S]*?<div class="simplebar-placeholder"[\s\S]*?</div>\s*</div>',
            '</ul>\n      </div>',
            content
        )

    with open(path, "w", encoding="utf-8") as f:
        f.write(content)
    print(f"Cleaned {path}")

print("Sidebars cleaned successfully.")
