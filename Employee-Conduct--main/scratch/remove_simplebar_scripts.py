import os
import glob

BASE_DIR = r"c:\Users\Dell\Desktop\Ronak Ec\Employee-Conduct--main"

html_files = glob.glob(os.path.join(BASE_DIR, "**", "*.html"), recursive=True)

count_css = 0
count_js = 0
for path in html_files:
    with open(path, "r", encoding="utf-8") as f:
        content = f.read()

    orig = content
    if "unpkg.com/simplebar@latest/dist/simplebar.css" in content:
        content = content.replace('    <link rel="stylesheet" href="https://unpkg.com/simplebar@latest/dist/simplebar.css"/>\n', '')
        content = content.replace('<link rel="stylesheet" href="https://unpkg.com/simplebar@latest/dist/simplebar.css"/>\n', '')
        content = content.replace('<link rel="stylesheet" href="https://unpkg.com/simplebar@latest/dist/simplebar.css"/>', '')
        count_css += 1

    if "unpkg.com/simplebar@latest/dist/simplebar.min.js" in content:
        content = content.replace('    <script src="https://unpkg.com/simplebar@latest/dist/simplebar.min.js"></script>\n', '')
        content = content.replace('<script src="https://unpkg.com/simplebar@latest/dist/simplebar.min.js"></script>\n', '')
        content = content.replace('<script src="https://unpkg.com/simplebar@latest/dist/simplebar.min.js"></script>', '')
        count_js += 1

    if content != orig:
        with open(path, "w", encoding="utf-8") as f:
            f.write(content)

print(f"Removed simplebar.css from {count_css} files, simplebar.min.js from {count_js} files.")
