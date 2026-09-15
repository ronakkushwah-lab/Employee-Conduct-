import os
import glob

BASE_DIR = r"c:\Users\Dell\Desktop\Ronak Ec\Employee-Conduct--main"

html_files = glob.glob(os.path.join(BASE_DIR, "**", "*.html"), recursive=True)

count = 0
for path in html_files:
    with open(path, "r", encoding="utf-8") as f:
        content = f.read()

    if "unpkg.com/boxicons@2.1.2/dist/boxicons.js" in content:
        content = content.replace('    <script src="https://unpkg.com/boxicons@2.1.2/dist/boxicons.js"></script>\n', '')
        content = content.replace('<script src="https://unpkg.com/boxicons@2.1.2/dist/boxicons.js"></script>\n', '')
        content = content.replace('<script src="https://unpkg.com/boxicons@2.1.2/dist/boxicons.js"></script>', '')
        with open(path, "w", encoding="utf-8") as f:
            f.write(content)
        count += 1
        print(f"Removed boxicons.js from {os.path.relpath(path, BASE_DIR)}")

print(f"Total files updated: {count}")
