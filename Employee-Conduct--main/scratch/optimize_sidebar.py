import os
import re

BASE_DIR = r"c:\Users\Dell\Desktop\Ronak Ec\Employee-Conduct--main"

# 1. Update style.css across all apps
style_files = [
    os.path.join(BASE_DIR, "administration", "static", "asets", "css", "style.css"),
    os.path.join(BASE_DIR, "employee", "static", "asets", "css", "style.css"),
    os.path.join(BASE_DIR, "leave", "static", "asets", "css", "style.css"),
    os.path.join(BASE_DIR, "management", "static", "asets", "css", "style.css"),
    os.path.join(BASE_DIR, "manager_leave", "static", "asets", "css", "style.css"),
    os.path.join(BASE_DIR, "managerpayroll", "static", "asets", "css", "style.css"),
    os.path.join(BASE_DIR, "managers", "static", "asets", "css", "style.css"),
    os.path.join(BASE_DIR, "payroll", "static", "asets", "css", "style.css"),
]

# Read original administration style.css
with open(style_files[0], "r", encoding="utf-8") as f:
    style_content = f.read()

# Replace sidebar base style section
old_sidebar_section_pattern = re.compile(
    r"/\* admin dashboard index page \*/\s*/\* sidebar style \*/\s*\.wrapper \.sidebar_wrapper\s*\{[\s\S]*?\.sidebar_wrapper \.metismenu\s*\{",
    re.MULTILINE
)

new_sidebar_section = """/* admin dashboard index page */
/* sidebar style */
.wrapper .sidebar_wrapper {
	width: 250px;
	height: 100vh;
	position: fixed;
	top: 0;
	bottom: 0;
	left: 0;
	background: linear-gradient(180deg, #1e293b 0%, #0f172a 100%);
	border-right: 0 solid #e4e4e4;
	z-index: 1001;
	box-shadow: 0 2px 12px rgba(0, 0, 0, 0.18);
	transition: width 0.25s cubic-bezier(0.4, 0, 0.2, 1), transform 0.25s cubic-bezier(0.4, 0, 0.2, 1), box-shadow 0.25s ease;
	will-change: width, transform;
	overflow-x: hidden;
	overflow-y: auto;
	scrollbar-width: thin;
	scrollbar-color: rgba(255, 255, 255, 0.2) transparent;
}
.wrapper .sidebar_wrapper::-webkit-scrollbar {
	width: 5px;
}
.wrapper .sidebar_wrapper::-webkit-scrollbar-track {
	background: transparent;
}
.wrapper .sidebar_wrapper::-webkit-scrollbar-thumb {
	background: rgba(255, 255, 255, 0.2);
	border-radius: 4px;
}
.wrapper .sidebar_wrapper .simplebar_wrapper,
.wrapper .sidebar_wrapper .simplebar-mask,
.wrapper .sidebar_wrapper .simplebar-offset,
.wrapper .sidebar_wrapper .simplebar-content-wrapper,
.wrapper .sidebar_wrapper .simplebar-content {
	height: auto;
	padding: 0;
	margin: 0;
}
.sidebar-header,
.simplebar-content .sidebar-header {
	width: 250px;
	height: 60px;
	display: flex;
	align-items: center;
	position: sticky;
	top: 0;
	padding: 0 15px;
	z-index: 5;
	background: #1e293b;
	border-bottom: 1px solid rgba(255, 255, 255, 0.08);
	transition: width 0.25s cubic-bezier(0.4, 0, 0.2, 1), padding 0.25s ease;
	overflow: hidden;
	flex-shrink: 0;
}
.sidebar-header img,
.simplebar-content .sidebar-header img {
	width: 150px;
	background: #fff;
	padding: 4px 6px;
	border-radius: 4px;
	filter: none !important;
	-webkit-filter: none !important;
	mix-blend-mode: normal !important;
	box-shadow: none !important;
	transition: opacity 0.2s ease, transform 0.2s ease;
}
.sidebar-header .toggle-icon,
.simplebar-content .sidebar-header .toggle-icon {
	font-size: 22px;
	cursor: pointer;
	color: #fff;
	display: flex;
	align-items: center;
	justify-content: center;
	transition: margin 0.25s ease;
}
.fa-regular {
	font-family: boxicons!important;
	font-weight: 400;
	font-style: normal;
	font-variant: normal;
	line-height: inherit;
	display: inline-block;
	text-transform: none;
	speak: none;
	-webkit-font-smoothing: antialiased;
}
.sidebar_wrapper .metismenu {"""

style_content = old_sidebar_section_pattern.sub(new_sidebar_section, style_content)

# Replace responsive sidebar style in style.css
old_responsive_sidebar_pattern = re.compile(
    r"/\* responsive sidebar style \*/\s*\.wrapper\.active \.sidebar_wrapper[\s\S]*?/\* page wrapper style \*/",
    re.MULTILINE
)

new_responsive_sidebar = """/* responsive sidebar style */
.wrapper.active .sidebar_wrapper {
    width: 70px;
}
.wrapper.active .sidebar-header,
.wrapper.active .simplebar-content .sidebar-header {
    width: 70px;
    padding: 0 10px;
}
.wrapper.active .sidebar-header img,
.wrapper.active .simplebar-content .sidebar-header img {
    opacity: 0;
    visibility: hidden;
    width: 0;
    padding: 0;
    margin: 0;
    display: none;
}
.wrapper.active .sidebar-header .toggle-icon,
.wrapper.active .simplebar-content .sidebar-header .toggle-icon {
    margin: 0 auto;
}
.sidebar-header .toggle-icon i#btn,
.simplebar-content .sidebar-header .toggle-icon i#btn {
    transition: transform 0.25s ease-out;
}
.wrapper.active .sidebar-header .toggle-icon i#btn,
.wrapper.active .simplebar-content .sidebar-header .toggle-icon i#btn {
    transform: rotate(180deg);
}
.wrapper.active .sidebar_wrapper .metisMenu .menu-label,
.wrapper.active .sidebar_wrapper .metismenu .menu-label,
.wrapper.active .sidebar_wrapper .metisMenu .menu-title,
.wrapper.active .sidebar_wrapper .metismenu .menu-title,
.wrapper.active .metismenu .mm-active>.has-arrow::after,
.wrapper.active .metismenu li>.has-arrow::after,
.wrapper.active .metismenu .has-arrow[aria-expanded=true]::after,
.wrapper.active .sidebar_wrapper .metismenu .mm-collapse {
    display: none !important;
}
.wrapper.active .sidebar_wrapper .metismenu>li>a {
    justify-content: center;
    padding: 10px 0;
}
.wrapper.active .sidebar_wrapper .metismenu>li>a .parent-icon {
    margin: 0 auto;
}

/* Hover over collapsed sidebar - smooth overlay expansion without reflowing .page_wrapper */
.wrapper.active .sidebar_wrapper:hover {
    width: 250px;
    box-shadow: 4px 0 24px rgba(0, 0, 0, 0.35);
}
.wrapper.active .sidebar_wrapper:hover .sidebar-header,
.wrapper.active .sidebar_wrapper:hover .simplebar-content .sidebar-header {
    width: 250px;
    padding: 0 15px;
    justify-content: space-between;
}
.wrapper.active .sidebar_wrapper:hover .sidebar-header img,
.wrapper.active .sidebar_wrapper:hover .simplebar-content .sidebar-header img {
    opacity: 1;
    visibility: visible;
    width: 150px;
    padding: 4px 6px;
    display: block;
}
.wrapper.active .sidebar_wrapper:hover .sidebar-header .toggle-icon,
.wrapper.active .sidebar_wrapper:hover .simplebar-content .sidebar-header .toggle-icon {
    margin-left: auto;
}
.wrapper.active .sidebar_wrapper:hover .metisMenu .menu-label,
.wrapper.active .sidebar_wrapper:hover .metismenu .menu-label,
.wrapper.active .sidebar_wrapper:hover .metisMenu .menu-title,
.wrapper.active .sidebar_wrapper:hover .metismenu .menu-title,
.wrapper.active .sidebar_wrapper:hover .metismenu .mm-active>.has-arrow::after,
.wrapper.active .sidebar_wrapper:hover .metismenu li>.has-arrow::after,
.wrapper.active .sidebar_wrapper:hover .metismenu .has-arrow[aria-expanded=true]::after {
    display: block !important;
}
.wrapper.active .sidebar_wrapper:hover .sidebar_wrapper .metismenu>li>a,
.wrapper.active .sidebar_wrapper:hover .metismenu>li>a {
    justify-content: flex-start;
    padding: 8px 15px;
}
.wrapper.active .sidebar_wrapper:hover .metismenu>li>a .parent-icon {
    margin: 0;
}
.wrapper.active .sidebar_wrapper:hover .metismenu .mm-collapse.mm-show {
    display: block !important;
}

/* Collapsed main layout spacing - stays fixed so hover does not cause full page reflow */
.wrapper.active .page_wrapper {
    width: calc(100% - 70px);
    margin-left: 70px;
    transition: margin-left 0.25s cubic-bezier(0.4, 0, 0.2, 1), width 0.25s cubic-bezier(0.4, 0, 0.2, 1);
}
.wrapper.active header .topbar,
.wrapper.active footer.page-footer {
    left: 70px;
    width: calc(100% - 70px);
    margin-left: 0;
    transition: left 0.25s cubic-bezier(0.4, 0, 0.2, 1), width 0.25s cubic-bezier(0.4, 0, 0.2, 1);
}

/* page wrapper style */"""

style_content = old_responsive_sidebar_pattern.sub(new_responsive_sidebar, style_content)

for path in style_files:
    with open(path, "w", encoding="utf-8") as f:
        f.write(style_content)
    print(f"Updated {path}")

# 2. Update responsive.css across all apps
responsive_files = [
    os.path.join(BASE_DIR, "administration", "static", "asets", "css", "responsive.css"),
    os.path.join(BASE_DIR, "employee", "static", "asets", "css", "responsive.css"),
    os.path.join(BASE_DIR, "leave", "static", "asets", "css", "responsive.css"),
    os.path.join(BASE_DIR, "management", "static", "asets", "css", "responsive.css"),
    os.path.join(BASE_DIR, "manager_leave", "static", "asets", "css", "responsive.css"),
    os.path.join(BASE_DIR, "managerpayroll", "static", "asets", "css", "responsive.css"),
    os.path.join(BASE_DIR, "managers", "static", "asets", "css", "responsive.css"),
    os.path.join(BASE_DIR, "payroll", "static", "asets", "css", "responsive.css"),
]

with open(responsive_files[0], "r", encoding="utf-8") as f:
    resp_content = f.read()

# Replace the first conflicting @media screen and (max-width: 838px) block (lines 28-77)
old_conflict_block_pattern = re.compile(
    r"@media screen and \(max-width:\s*838px\)\s*\{\s*\.wrapper \.sidebar_wrapper[\s\S]*?@media screen and \(max-width:\s*768px\)",
    re.MULTILINE
)

clean_conflict_block = """@media screen and (max-width: 838px) {
	.wrapper header .topbar .navbar .search-bar .position-relative.search-bar-box {
	    width: 100% !important;
	}
	.accordion-button {
		width: 100% !important;
	}
	#createinvoiceModal .table tr th {
		min-width: 200px !important;
	}
	#createinvoiceModal .table tr th:last-child {
		min-width: auto !important;
	}
}

@media screen and (max-width: 768px)"""

resp_content = old_conflict_block_pattern.sub(clean_conflict_block, resp_content)

for path in responsive_files:
    with open(path, "w", encoding="utf-8") as f:
        f.write(resp_content)
    print(f"Updated {path}")

# 3. Update script.js across all apps
script_files = [
    os.path.join(BASE_DIR, "administration", "static", "asets", "js", "script.js"),
    os.path.join(BASE_DIR, "employee", "static", "asets", "js", "script.js"),
    os.path.join(BASE_DIR, "leave", "static", "asets", "js", "script.js"),
    os.path.join(BASE_DIR, "management", "static", "asets", "js", "script.js"),
    os.path.join(BASE_DIR, "manager_leave", "static", "asets", "js", "script.js"),
    os.path.join(BASE_DIR, "managerpayroll", "static", "asets", "js", "script.js"),
    os.path.join(BASE_DIR, "managers", "static", "asets", "js", "script.js"),
    os.path.join(BASE_DIR, "payroll", "static", "asets", "js", "script.js"),
]

for path in script_files:
    with open(path, "r", encoding="utf-8") as f:
        content = f.read()
    # Replace sidebar responsive script
    old_script_pattern = re.compile(
        r'/\* sidebar responsive script \*/\s*\$\("#btn"\)\.click\(function\(\)\{\s*\$\("\.wrapper"\)\.toggleClass\("active"\);\s*\$\("\.sidebar_wrapper \.toggle-icon"\)\.removeClass\("ms-auto"\);\s*\$\("\.sidebar_wrapper \.toggle-icon"\)\.addClass\("m-auto"\);\s*\}\);',
        re.MULTILINE
    )
    new_script = """/* sidebar responsive script */
$("#btn").click(function(){
    $(".wrapper").toggleClass("active");
});"""
    content = old_script_pattern.sub(new_script, content)
    with open(path, "w", encoding="utf-8") as f:
        f.write(content)
    print(f"Updated {path}")

print("All static assets updated successfully.")
