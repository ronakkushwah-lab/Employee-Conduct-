FROM python:3.10-slim

# Set environment variables
ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1

# Set Timezone
ENV TZ=Asia/Kolkata

# Set working directory
WORKDIR /app

# Copy requirements from Employee-Conduct--main
COPY Employee-Conduct--main/requirements.txt /app/
RUN pip install --upgrade pip && pip install -r requirements.txt

# Copy all application files from Employee-Conduct--main
COPY Employee-Conduct--main/ /app/

# Set PYTHONPATH to application directory
ENV PYTHONPATH=/app

# Run Django collectstatic to compile static files during image build
RUN python manage.py collectstatic --noinput

# Expose the port
EXPOSE 8000

# Default command
CMD ["sh", "-c", "python manage.py migrate --noinput && python -m gunicorn --bind 0.0.0.0:${PORT:-8000} --pythonpath /app -w 5 dstt.wsgi:application"]
