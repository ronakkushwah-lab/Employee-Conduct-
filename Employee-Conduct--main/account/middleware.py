from django.shortcuts import redirect
from django.utils.deprecation import MiddlewareMixin

from .models import CompanyStaff


class SessionSecurityMiddleware(MiddlewareMixin):
    """
    Enforce session-based security for admin/manager/employee areas and
    prevent cached pages from acting like active sessions after logout.
    """

    PROTECTED_PREFIXES = ("/administration/", "/managers/", "/employee/")

    def _is_protected_path(self, path: str) -> bool:
        return any(path.startswith(prefix) for prefix in self.PROTECTED_PREFIXES)

    def process_view(self, request, view_func, view_args, view_kwargs):
        """
        Before calling the view, ensure that:
        - For protected URLs, there is a valid CompanyStaff in session
          with is_authenticated=True.
        - Otherwise redirect to login page ('/').
        """
        path = request.path or ""

        # Skip for non-protected paths (login, signup, password reset, static, admin, etc.)
        if not self._is_protected_path(path):
            return None

        # Static and media should never be forced through auth
        if path.startswith("/static/") or path.startswith("/media/"):
            return None

        company_staff_id = request.session.get("company_staff_id")
        if not company_staff_id:
            # No staff in session → force login
            request.session.flush()
            return redirect("/")

        try:
            staff = CompanyStaff.objects.get(pk=company_staff_id)
        except CompanyStaff.DoesNotExist:
            request.session.flush()
            return redirect("/")

        # If our custom auth flag is false, treat as logged out
        if not staff.is_authenticated or not staff.is_active:
            request.session.flush()
            return redirect("/")

        # Allow request to proceed
        return None

    def process_response(self, request, response):
        """
        Disable caching for protected pages so that browser back button
        forces a new request (and thus our auth checks).
        """
        path = getattr(request, "path", "") or ""
        if self._is_protected_path(path):
            response["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0"
            response["Pragma"] = "no-cache"
            response["Expires"] = "0"
        return response

