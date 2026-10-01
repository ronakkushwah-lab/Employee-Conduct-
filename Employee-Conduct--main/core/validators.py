import re

def validate_password_strength(password):
    """
    Validates that a password meets company security standards:
    1. Minimum 8 characters in length.
    2. Contains at least one letter (a-z or A-Z).
    3. Contains at least one numeric digit (0-9).
    
    Returns:
        tuple: (is_valid: bool, error_message: str)
    """
    if not password:
        return False, "Password is required."
    
    if len(password) < 8:
        return False, "Password must be at least 8 characters long."
    
    if not re.search(r'[A-Za-z]', password):
        return False, "Password must contain at least one letter (a-z, A-Z)."
    
    if not re.search(r'\d', password):
        return False, "Password must contain at least one digit/number (0-9)."
    
    return True, ""
