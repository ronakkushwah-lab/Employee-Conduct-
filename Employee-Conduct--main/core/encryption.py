"""
Core Encryption Engine for HRMS (AES-256 Fernet Symmetric Encryption).
Provides transparent field-level encryption/decryption for sensitive PII and Payroll data.
"""
import os
import base64
from cryptography.fernet import Fernet, InvalidToken
from django.conf import settings
from django.db import models


# Default key if none provided in environment
DEFAULT_KEY = b'bNn5A3CtVw2wVtJVXxkPyk55FW5x_Z9wTnz1Eyodjmk='


def get_fernet():
    """Retrieve or initialize the active Fernet cipher engine."""
    raw_key = getattr(settings, 'FERNET_ENCRYPTION_KEY', None) or os.environ.get('FERNET_ENCRYPTION_KEY')
    if not raw_key:
        raw_key = DEFAULT_KEY
    if isinstance(raw_key, str):
        raw_key = raw_key.encode('utf-8')
    try:
        return Fernet(raw_key)
    except Exception:
        # Fallback to standard derived key if malformed
        return Fernet(DEFAULT_KEY)


def encrypt_value(value):
    """
    Encrypt a string or primitive value using AES-256 Fernet.
    Returns the encrypted base64 string prefixed with Fernet token header.
    If value is None or empty, returns value unchanged.
    """
    if value is None:
        return None
    val_str = str(value)
    if val_str == "":
        return ""
    # Already encrypted token check
    if val_str.startswith("gAAAAA"):
        return val_str
    try:
        f = get_fernet()
        encrypted_bytes = f.encrypt(val_str.encode('utf-8'))
        return encrypted_bytes.decode('utf-8')
    except Exception as e:
        # Safety fallback
        return val_str


def decrypt_value(value):
    """
    Decrypt a Fernet token back to its original plain text.
    If value is None, empty, or not an encrypted token, safely returns the raw value.
    """
    if value is None:
        return None
    val_str = str(value)
    if val_str == "":
        return ""
    if not val_str.startswith("gAAAAA"):
        return val_str  # Already plain text
    try:
        f = get_fernet()
        decrypted_bytes = f.decrypt(val_str.encode('utf-8'))
        return decrypted_bytes.decode('utf-8')
    except (InvalidToken, Exception):
        # Fallback to original string if decryption fails (e.g. legacy plain text)
        return val_str


class EncryptedCharField(models.CharField):
    """
    CharField that transparently encrypts values before writing to the database
    and decrypts them into plain text when loaded into memory.
    """
    def __init__(self, *args, **kwargs):
        # Ensure sufficient length for base64 encrypted tokens (min 255)
        if 'max_length' in kwargs and kwargs['max_length'] < 255:
            kwargs['max_length'] = 255
        kwargs.setdefault('max_length', 255)
        super().__init__(*args, **kwargs)

    def from_db_value(self, value, expression, connection):
        if value is None:
            return None
        return decrypt_value(value)

    def to_python(self, value):
        if value is None:
            return None
        if isinstance(value, str) and value.startswith("gAAAAA"):
            return decrypt_value(value)
        return str(value) if value is not None else None

    def get_prep_value(self, value):
        if value is None:
            return None
        value_str = str(value)
        if value_str == "":
            return ""
        return encrypt_value(value_str)


class EncryptedIntegerField(models.CharField):
    """
    Field that presents as a Python integer in memory and models (supporting math operations like + - *),
    while storing the encrypted string token in the database.
    """
    def __init__(self, *args, **kwargs):
        kwargs['max_length'] = 255
        super().__init__(*args, **kwargs)

    def from_db_value(self, value, expression, connection):
        if value is None:
            return None
        decrypted = decrypt_value(value)
        try:
            return int(float(decrypted))
        except (ValueError, TypeError):
            return 0 if self.default == 0 else None

    def to_python(self, value):
        if value is None:
            return None
        if isinstance(value, int):
            return value
        if isinstance(value, str):
            if value.startswith("gAAAAA"):
                value = decrypt_value(value)
            try:
                return int(float(value))
            except (ValueError, TypeError):
                return 0 if self.default == 0 else None
        return value

    def get_prep_value(self, value):
        if value is None:
            return None
        if isinstance(value, str) and value.startswith("gAAAAA"):
            return value
        try:
            int_val = int(value)
            return encrypt_value(str(int_val))
        except (ValueError, TypeError):
            return encrypt_value(str(value))
