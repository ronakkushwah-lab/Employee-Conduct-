from math import ceil
from django.http.response import HttpResponse
import json
from django.forms.models import model_to_dict
from django.utils.html import escape


def strfdelta(tdelta, fmt):
    d = {}
    d["hours"], rem = divmod(tdelta.seconds, 3600)
    d["minutes"], d["seconds"] = divmod(rem, 60)
    return fmt.format(**d)


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
