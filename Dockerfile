FROM python:3.10-slim

# Set environment variables
ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1

# Set Timezone
ENV TZ=Asia/Kolkata

# Set working directory
WORKDIR /app

# Copy only requirements.txt initially to leverage caching
COPY requirements.txt /app/
RUN pip install --upgrade pip && pip install -r requirements.txt
# RUN pip install xgboost --prefer-binary

# Now copy the rest of the application files
COPY . /app/

# Expose the port
EXPOSE 8000

# Default command (can be overridden in docker-compose.yml)
CMD ["gunicorn", "--bind", "0.0.0.0:8000", "-w", "5", "dstt.wsgi:application"]