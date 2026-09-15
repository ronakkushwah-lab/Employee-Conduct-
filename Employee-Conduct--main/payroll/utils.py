
from io import BytesIO  # A stream implementation using an in-memory bytes buffer
# It inherits BufferIOBase

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

# xhtml2pdf is imported inside render_to_pdf() to avoid loading reportlab/_renderPM
# at server start (Windows Application Control can block that DLL).


def render_to_pdf(context=dict):
    # Add logo as base64 to context for PDF (before any pisa import so we can use context in fallback)
    # Try multiple possible logo locations - prioritize company logo (EAGLE IN CLOUD)
    logo_paths = [
        os.path.join(BASE_DIR, 'payroll', 'static', 'asets', 'images', 'compny_logo.png'),  # EAGLE IN CLOUD logo
        os.path.join(BASE_DIR, 'employee', 'static', 'asets', 'images', 'compny_logo.png'),
        os.path.join(BASE_DIR, 'employee', 'static', 'asets', 'images', 'employee_conduct_logo.png'),
        os.path.join(BASE_DIR, 'payroll', 'static', 'asets', 'images', 'employee_conduct_logo.png'),
    ]
    
    logo_base64 = ''
    logo_path = None
    
    # Find the first existing logo file
    for path in logo_paths:
        if os.path.exists(path):
            logo_path = path
            break
    
    if logo_path:
        try:
            with open(logo_path, 'rb') as logo_file:
                logo_data = logo_file.read()
                logo_base64 = base64.b64encode(logo_data).decode('utf-8')
                print(f"Successfully loaded logo from: {logo_path}")
        except Exception as e:
            print(f"Error loading logo from {logo_path}: {e}")
    else:
        print("Warning: Logo file not found in any of the expected locations")
        for path in logo_paths:
            print(f"  Checked: {path}")
    
    context['logo_base64'] = logo_base64
    
    template = get_template("payroll/pdf_template.html")
    html = template.render(context)
    
    # Replace problematic unicode characters with ASCII equivalents
    html = html.replace('\u2013', '-')  # en dash to hyphen
    html = html.replace('\u2014', '--')  # em dash to double hyphen
    html = html.replace('\u2018', "'")  # left single quotation mark
    html = html.replace('\u2019', "'")  # right single quotation mark
    html = html.replace('\u201C', '"')  # left double quotation mark
    html = html.replace('\u201D', '"')  # right double quotation mark
    # Convert HTML entity for rupee symbol to actual unicode character
    html = html.replace('&#8377;', '₹')  # Convert HTML entity to unicode rupee symbol
    html = html.replace('&#x20B9;', '₹')  # Convert hex HTML entity to unicode

    # Try PDF generation (xhtml2pdf uses reportlab; on Windows, _renderPM DLL can be blocked by policy)
    try:
        from xhtml2pdf import pisa
    except ImportError as e:
        print(f"xhtml2pdf/reportlab not available ({e}). Returning salary slip as HTML - use browser Print (Ctrl+P) > Save as PDF.")
        resp = HttpResponse(html, content_type='text/html; charset=utf-8')
        resp['Content-Disposition'] = 'inline; filename="salary-slip.html"'
        return resp

    result = BytesIO()
    try:
        html_bytes = html.encode("UTF-8")
        pdf = pisa.pisaDocument(BytesIO(html_bytes), result, encoding='UTF-8')
        if not pdf.err:
            pdf_content = result.getvalue()
            if pdf_content:
                return HttpResponse(pdf_content, content_type='application/pdf')
            else:
                print("Error: PDF generation resulted in empty content")
        else:
            print(f"PDF generation errors: {pdf.err}")
            pdf_content = result.getvalue()
            if pdf_content:
                return HttpResponse(pdf_content, content_type='application/pdf')
    except Exception as e:
        print(f"PDF generation failed: {e}. Returning salary slip as HTML - use browser Print (Ctrl+P) > Save as PDF.")
        resp = HttpResponse(html, content_type='text/html; charset=utf-8')
        resp['Content-Disposition'] = 'inline; filename="salary-slip.html"'
        return resp

    # Fallback: return HTML so user can at least see the slip and print to PDF from browser
    print("PDF generation produced no content. Returning salary slip as HTML.")
    resp = HttpResponse(html, content_type='text/html; charset=utf-8')
    resp['Content-Disposition'] = 'inline; filename="salary-slip.html"'
    return resp


def render_slip_html(context):
    """Render salary slip as HTML only (no PDF). Used when opening slip in tab for view."""
    logo_paths = [
        os.path.join(BASE_DIR, 'payroll', 'static', 'asets', 'images', 'compny_logo.png'),
        os.path.join(BASE_DIR, 'employee', 'static', 'asets', 'images', 'compny_logo.png'),
        os.path.join(BASE_DIR, 'employee', 'static', 'asets', 'images', 'employee_conduct_logo.png'),
        os.path.join(BASE_DIR, 'payroll', 'static', 'asets', 'images', 'employee_conduct_logo.png'),
    ]
    logo_base64 = ''
    for path in logo_paths:
        if os.path.exists(path):
            try:
                with open(path, 'rb') as f:
                    logo_base64 = base64.b64encode(f.read()).decode('utf-8')
            except Exception:
                pass
            break
    context['logo_base64'] = logo_base64
    template = get_template("payroll/pdf_template.html")
    html = template.render(context)
    html = html.replace('\u2013', '-').replace('\u2014', '--')
    html = html.replace('\u2018', "'").replace('\u2019', "'")
    html = html.replace('\u201C', '"').replace('\u201D', '"')
    html = html.replace('&#8377;', '₹').replace('&#x20B9;', '₹')
    resp = HttpResponse(html, content_type='text/html; charset=utf-8')
    resp['Content-Disposition'] = 'inline; filename="salary-slip.html"'
    return resp
