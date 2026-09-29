from django.http import HttpResponseRedirect
from django.shortcuts import redirect
from django.utils.deprecation import MiddlewareMixin

from .models import CompanyStaff


class HttpsDomainRedirectMiddleware(MiddlewareMixin):
    """
    If a user accesses the site via raw IP / AWS hostname (e.g. 50.19.21.0:8001 or ec2-*.amazonaws.com:8001),
    automatically redirect their browser to the canonical secure domain https://eagleinclouds.com/hrms/.

    Exemptions:
    - Biometric hardware attendance pushes (/iclock/, /cdata/, /push/, etc.)
    - Localhost testing (localhost, 127.0.0.1)
    """
    EXEMPT_PREFIXES = ('/iclock', '/cdata', '/push', '/api/device', '/static/', '/media/')

    def process_request(self, request):
        host = request.get_host().lower().split(':')[0]
        path = request.path or '/'

        # Only redirect for raw public IP or AWS public DNS
        is_direct_ip_or_aws = (
            'amazonaws.com' in host
            or host == '50.19.21.0'
            or host == '65.0.32.183'
        )

        if not is_direct_ip_or_aws:
            return None

        # Do not redirect biometric machine pushes or static assets
        if any(path.startswith(prefix) for prefix in self.EXEMPT_PREFIXES):
            return None

        # If it's a POST/PUT/DELETE request (e.g. hardware push), don't redirect
        if request.method not in ('GET', 'HEAD'):
            return None

        # Build clean target URL on https://eagleinclouds.com
        if path.startswith('/hrms'):
            target_url = f'https://eagleinclouds.com{path}'
        else:
            clean_path = path if path != '/' else ''
            target_url = f'https://eagleinclouds.com/hrms{clean_path}/' if not clean_path.endswith('/') else f'https://eagleinclouds.com/hrms{clean_path}'

        if request.META.get('QUERY_STRING'):
            target_url = f"{target_url}?{request.META['QUERY_STRING']}"

        return HttpResponseRedirect(target_url)



class SessionSecurityMiddleware(MiddlewareMixin):
    """
    Enforce session-based security for admin/manager/employee areas and
    prevent cached pages from acting like active sessions after logout.
    """

    PROTECTED_PREFIXES = ("/administration/", "/managers/", "/employee/", "/dashboard/")

    def _is_protected_path(self, path: str) -> bool:
        return any(path.startswith(prefix) for prefix in self.PROTECTED_PREFIXES)

    def process_view(self, request, view_func, view_args, view_kwargs):
        """
        Before calling the view, ensure that:
        - If password has expired, force user to expired_password page.
        - For protected URLs, there is a valid CompanyStaff in session
          with is_authenticated=True and active password within 60 days.
        - Otherwise redirect to login page ('/').
        """
        path = request.path or ""

        # Static and media should never be forced through auth
        if path.startswith("/static/") or path.startswith("/media/"):
            return None

        # If user session has password_expired flagged, lock them to the expired password screen
        if request.session.get("password_expired"):
            if not path.startswith("/expired_password") and not path.startswith("/logout"):
                return redirect("/expired_password/")

        # Skip for non-protected paths (login, signup, password reset, admin, etc.)
        if not self._is_protected_path(path):
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

        # Check 60-day password expiration
        if staff.is_password_expired(expiry_days=60):
            request.session["password_expired"] = True
            return redirect("/expired_password/")

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

