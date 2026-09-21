from math import ceil
from datetime import timedelta
import re
from django.http.response import HttpResponse
import json
from django.forms.models import model_to_dict
from django.utils.html import escape
from django.contrib import messages


def strfdelta(tdelta, fmt="{hours}.{minutes}"):
    if not tdelta or not isinstance(tdelta, timedelta):
        return ''
    try:
        total_seconds = int(tdelta.total_seconds())
        hours, rem = divmod(abs(total_seconds), 3600)
        minutes, seconds = divmod(rem, 60)
        
        d = {
            "hours": f"{hours:02d}",
            "minutes": f"{minutes:02d}",
            "seconds": f"{seconds:02d}"
        }
        
        try:
            keys_in_fmt = re.findall(r'\{(\w+)\}', fmt)
            filtered_d = {key: d[key] for key in keys_in_fmt if key in d}
            if filtered_d:
                return fmt.format(**filtered_d)
            return f"{hours:02d}.{minutes:02d}"
        except Exception:
            return f"{hours:02d}.{minutes:02d}"
    except Exception:
        return ''


def show_message_once(request, message, message_type='error', session_key='message_shown'):
    """
    Show a message only once per session to avoid duplicates.
    Returns True if message was shown, False if it was already shown.
    """
    session_key_full = f'{session_key}_{message_type}'
    if not request.session.get(session_key_full, False):
        if message_type == 'error':
            messages.error(request, message)
        elif message_type == 'success':
            messages.success(request, message)
        elif message_type == 'warning':
            messages.warning(request, message)
        else:
            messages.info(request, message)
        request.session[session_key_full] = True
        return True
    return False


def getgriddatapaginated(request, rs, sort_column):
    rows = int(request.GET['length'])
    page = int(request.GET['start'])
    sort_by = 'id' if not sort_column else sort_column
    sord = request.GET['order[0][dir]']
    end = rows + page
    tototal_records = rs.count()
    sortOn = "-" + sort_by if sord == "desc" else sort_by
    rs = rs.order_by(sortOn)[page: end]
    ctx = {}
    ctx['draw'] = request.GET['draw']
    ctx['recordsFiltered'] = tototal_records
    ctx['recordsTotal'] = tototal_records
    ctx['data'] = rs
    return ctx


def ajax_response(data):
    response = HttpResponse(json.dumps(data, ensure_ascii=False, default=json_default_fn),
                            content_type='application/json')
    return response


def json_default_fn(obj):
    if hasattr(obj, 'isoformat'):
        return obj.isoformat()
    else:
        obj
