from io import BytesIO  # A stream implementation using an in-memory bytes buffer

from django.http import HttpResponse
from django.template.loader import get_template
from django.conf import settings
import os
import base64

# Get BASE_DIR from settings or calculate it
try:
    BASE_DIR = settings.BASE_DIR
except AttributeError:
    BASE_DIR = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))


def _get_logo_base64():
    logo_paths = [
        os.path.join(BASE_DIR, 'payroll', 'static', 'asets', 'images', 'eagle_in_cloud_logo.png'),
        os.path.join(BASE_DIR, 'employee', 'static', 'asets', 'images', 'eagle_in_cloud_logo.png'),
        os.path.join(BASE_DIR, 'payroll', 'static', 'asets', 'images', 'compny_logo.png'),
        os.path.join(BASE_DIR, 'employee', 'static', 'asets', 'images', 'compny_logo.png'),
        os.path.join(BASE_DIR, 'employee', 'static', 'asets', 'images', 'employee_conduct_logo.png'),
        os.path.join(BASE_DIR, 'payroll', 'static', 'asets', 'images', 'employee_conduct_logo.png'),
    ]

    for path in logo_paths:
        if os.path.exists(path):
            try:
                with open(path, 'rb') as f:
                    return base64.b64encode(f.read()).decode('utf-8')
            except Exception:
                pass
    return ''


def render_to_pdf(context=dict):
    """
    Generate manager salary slip PDF using xhtml2pdf.
    If xhtml2pdf/reportlab is blocked, fall back to returning HTML.
    """
    context['logo_base64'] = _get_logo_base64()

    template = get_template("managerpayroll/pdf_template.html")
    html = template.render(context)

    # Replace problematic unicode characters with ASCII equivalents
    html = html.replace("\u2013", "-")  # en dash
    html = html.replace("\u2014", "--")  # em dash
    html = html.replace("\u2018", "'").replace("\u2019", "'")  # single quotes
    html = html.replace("\u201C", '"').replace("\u201D", '"')  # double quotes
    html = html.replace("&#8377;", "₹").replace("&#x20B9;", "₹")  # rupee entities

    try:
        from xhtml2pdf import pisa
    except ImportError as e:
        print(
            f"xhtml2pdf/reportlab not available for managerpayroll ({e}). "
            "Returning salary slip as HTML - use browser Print (Ctrl+P) > Save as PDF."
        )
        resp = HttpResponse(html, content_type="text/html; charset=utf-8")
        resp["Content-Disposition"] = 'inline; filename="manager-salary-slip.html"'
        return resp

    result = BytesIO()
    try:
        html_bytes = html.encode("UTF-8")
        pdf = pisa.pisaDocument(BytesIO(html_bytes), result, encoding="UTF-8")
        if not pdf.err:
            pdf_content = result.getvalue()
            if pdf_content:
                return HttpResponse(pdf_content, content_type="application/pdf")
            else:
                print("Managerpayroll PDF generation resulted in empty content")
        else:
            print(f"Managerpayroll PDF generation errors: {pdf.err}")
            pdf_content = result.getvalue()
            if pdf_content:
                return HttpResponse(pdf_content, content_type="application/pdf")
    except Exception as e:
        print(
            f"Managerpayroll PDF generation failed: {e}. "
            "Returning salary slip as HTML - use browser Print (Ctrl+P) > Save as PDF."
        )
        resp = HttpResponse(html, content_type="text/html; charset=utf-8")
        resp["Content-Disposition"] = 'inline; filename="manager-salary-slip.html"'
        return resp

    # Fallback: return HTML so user can at least see the slip
    print("Managerpayroll PDF generation produced no content. Returning salary slip as HTML.")
    resp = HttpResponse(html, content_type="text/html; charset=utf-8")
    resp["Content-Disposition"] = 'inline; filename="manager-salary-slip.html"'
    return resp


def render_slip_html(context):
    """Render manager salary slip as HTML only (no PDF). Used when opening slip in tab for view."""
    context['logo_base64'] = _get_logo_base64()
    template = get_template("managerpayroll/pdf_template.html")
    html = template.render(context)
    html = html.replace('\u2013', '-').replace('\u2014', '--')
    html = html.replace('\u2018', "'").replace('\u2019', "'")
    html = html.replace('\u201C', '"').replace('\u201D', '"')
    html = html.replace('&#8377;', '₹').replace('&#x20B9;', '₹')
    resp = HttpResponse(html, content_type='text/html; charset=utf-8')
    resp['Content-Disposition'] = 'inline; filename="manager-salary-slip.html"'
    return resp

