class NormalizeDoubleSlashMiddleware:
    def __init__(self, get_response):
        self.get_response = get_response

    def __call__(self, request):
        if request.path_info.startswith('//'):
            normalized = '/' + request.path_info.lstrip('/')
            request.path_info = normalized
            request.path = normalized

        # Disable CSRF / Referer checks for all biometric hardware endpoints
        clean_path = request.path_info.lower()
        if (
            'iclock' in clean_path
            or 'cdata' in clean_path
            or 'adms' in clean_path
            or 'getrequest' in clean_path
            or 'devicecmd' in clean_path
            or 'fdata' in clean_path
            or 'push' in clean_path
            or clean_path.endswith('.php')
            or clean_path == '/'
        ):
            request._dont_enforce_csrf_checks = True

        return self.get_response(request)
