from math import ceil
from django.http.response import HttpResponse
import json
from django.forms.models import model_to_dict
from django.utils.html import escape


def strfdelta(tdelta, fmt="{hours}.{minutes}"):
    if not tdelta:
        return ''
    try:
        total_seconds = int(tdelta.total_seconds()) if hasattr(tdelta, 'total_seconds') else getattr(tdelta, 'seconds', 0)
        hours, rem = divmod(abs(total_seconds), 3600)
        minutes, seconds = divmod(rem, 60)
        d = {
            "hours": f"{hours:02d}",
            "minutes": f"{minutes:02d}",
            "seconds": f"{seconds:02d}"
        }
        try:
            return fmt.format(**d)
        except Exception:
            return f"{hours:02d}.{minutes:02d}"
    except Exception:
        return ''


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
