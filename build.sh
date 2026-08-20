#!/usr/bin/env bash
# Build script for Render deployment

# Ensure we're using the correct Python version
python3 --version

# Upgrade pip, setuptools, and wheel first
pip install --upgrade pip
pip install "setuptools<70.0.0" wheel

# Install packages that might need special handling
# Try to install from wheels first to avoid building from source
pip install --only-binary :all: -r requirements.txt || pip install -r requirements.txt

