
from io import BytesIO  # A stream implementation using an in-memory bytes buffer

from django.http import HttpResponse
from django.template.loader import get_template
from django.conf import settings
import os
import base64


def render_to_pdf(context=dict):
    """
    Generate manager salary slip PDF using xhtml2pdf.
    If xhtml2pdf/reportlab is blocked (Windows _renderPM DLL issue),
    fall back to returning the HTML so it can be printed/saved from browser.
    """
    # Embed logo as base64 so xhtml2pdf doesn't depend on fetching static URLs.
    try:
        base_dir = settings.BASE_DIR
    except AttributeError:
        # Fallback for environments where settings.BASE_DIR isn't available.
        base_dir = os.path.dirname(os.path.dirname(os.path.dirname(os.path.abspath(__file__))))

    logo_paths = [
        os.path.join(base_dir, 'payroll', 'static', 'asets', 'images', 'compny_logo.png'),
        os.path.join(base_dir, 'employee', 'static', 'asets', 'images', 'compny_logo.png'),
    ]

    logo_base64 = ''
    for path in logo_paths:
        if os.path.exists(path):
            try:
                with open(path, 'rb') as f:
                    logo_base64 = base64.b64encode(f.read()).decode('utf-8')
            except Exception:
                logo_base64 = ''
            break

    context['logo_base64'] = logo_base64

    template = get_template("managerpayroll/pdf_template.html")
    html = template.render(context)

    # Replace problematic unicode characters with ASCII equivalents
    html = html.replace("\u2013", "-")  # en dash
    html = html.replace("\u2014", "--")  # em dash
    html = html.replace("\u2018", "'").replace("\u2019", "'")  # single quotes
    html = html.replace("\u201C", '"').replace("\u201D", '"')  # double quotes
    html = html.replace("&#8377;", "₹").replace("&#x20B9;", "₹")  # rupee entities

    try:
        # Import inside try so DLL load errors are caught and we can fall back to HTML
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
