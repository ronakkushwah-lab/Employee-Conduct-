from io import BytesIO  # A stream implementation using an in-memory bytes buffer
# It inherits BufferIOBase

from django.http import HttpResponse
from django.template.loader import get_template

# pisa is imported inside render_to_pdf() to avoid loading reportlab/_renderPM at
# server start (Windows can block that DLL via Application Control policy).


def render_to_pdf(context=dict):
    from xhtml2pdf import pisa

    template = get_template("management/view-invoice.html")
    html = template.render(context)
    result = BytesIO()

    # This part will create the pdf.
    pdf = pisa.pisaDocument(BytesIO(html.encode("ISO-8859-1")), result)
    if not pdf.err:
        response = HttpResponse(result.getvalue(), content_type='application/pdf')
        return response
    return None
    