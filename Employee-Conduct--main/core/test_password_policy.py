import os
import sys

proj_dir = r"c:\Users\Dell\OneDrive\Desktop\Ronak Ec\Employee-Conduct--main"
if proj_dir not in sys.path:
    sys.path.insert(0, proj_dir)

from core.validators import validate_password_strength

test_cases = [
    # (password, expected_valid, note)
    ("", False, "Empty password"),
    ("short1", False, "Too short (< 8 chars)"),
    ("12345678", False, "Only digits (no letters)"),
    ("abcdefgh", False, "Only letters (no digits)"),
    ("ABCDEFGH", False, "Only uppercase letters (no digits)"),
    ("Pass1234", True, "Valid mixed letters & digits (8 chars)"),
    ("Eagle@2026", True, "Valid mixed letters, digits & special (10 chars)"),
    ("strongpassword1", True, "Valid long password with letters and digit"),
    ("987654321a", True, "Valid digits first with letter at end"),
]

print("=== RUNNING PASSWORD POLICY VALIDATION TESTS ===")
passed = 0
for pwd, exp_valid, note in test_cases:
    is_valid, msg = validate_password_strength(pwd)
    status = "PASS" if is_valid == exp_valid else "FAIL"
    if status == "PASS":
        passed += 1
    print(f"[{status}] Password: '{pwd}' -> Valid: {is_valid} (Expected: {exp_valid}) | Msg: '{msg}' | Note: {note}")

print(f"\nResults: {passed}/{len(test_cases)} tests passed.")
assert passed == len(test_cases), "Some password policy tests failed!"
print("ALL PASSWORD POLICY UNIT TESTS PASSED SUCCESSFULLY!")
