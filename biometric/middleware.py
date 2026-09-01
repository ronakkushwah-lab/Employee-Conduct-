class NormalizeDoubleSlashMiddleware:
    def __init__(self, get_response):
        self.get_response = get_response

    def __call__(self, request):
        if request.path_info.startswith('//'):
            request.path_info = '/' + request.path_info.lstrip('/')
        return self.get_response(request)
