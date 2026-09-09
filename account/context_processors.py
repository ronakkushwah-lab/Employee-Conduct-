"""
Context processors for account app templates.
"""

from __future__ import annotations

from typing import Any, Dict, Optional

from account.models import CompanyStaff


def admin_company_staff(request):
    """
    Add admin/company staff related context for templates.
    This project often tracks auth via `CompanyStaff` + session keys, so we
    expose the current `CompanyStaff` (if any) for consistent template behavior.
    """
    company_staff: Optional[CompanyStaff] = None

    try:
        company_staff_id = None

        # Preferred: set by login flow in session
        if hasattr(request, "session"):
            company_staff_id = request.session.get("company_staff_id")

        # Fallback: some views pass it via URL kwargs
        if not company_staff_id and getattr(request, "resolver_match", None):
            company_staff_id = request.resolver_match.kwargs.get("company_staff_id")

        if company_staff_id:
            try:
                company_staff_id = int(company_staff_id)
                company_staff = CompanyStaff.objects.filter(id=company_staff_id).first()
            except (TypeError, ValueError):
                company_staff = None
    except Exception:
        company_staff = None

    company_staff_authenticated = bool(company_staff and company_staff.is_authenticated and company_staff.is_active)
    company_staff_is_admin = bool(company_staff_authenticated and (company_staff.is_company_admin or company_staff.role == CompanyStaff.ROLE_ADMIN))
    company_staff_is_hr = bool(company_staff_authenticated and (getattr(company_staff, 'is_hr', False) or getattr(company_staff, 'role', '') == 'hr'))

    context: Dict[str, Any] = {
        "staff": company_staff,
        "company_staff": company_staff,
        "company_staff_authenticated": company_staff_authenticated,
        "company_staff_is_admin": company_staff_is_admin,
        "company_staff_is_hr": company_staff_is_hr,
    }
    return context
