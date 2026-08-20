def number_to_words(num):
    """Convert number to words in Indian format"""
    # Handle if num is a method or property
    if callable(num):
        num = num()
    
    # Convert to int/float if string
    try:
        num = float(num) if num else 0
        num = int(round(num))
    except (ValueError, TypeError):
        return 'Zero Only'
    
    ones = ['', 'One', 'Two', 'Three', 'Four', 'Five', 'Six', 'Seven', 'Eight', 'Nine', 'Ten',
            'Eleven', 'Twelve', 'Thirteen', 'Fourteen', 'Fifteen', 'Sixteen', 'Seventeen',
            'Eighteen', 'Nineteen']
    tens = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety']
    
    if num == 0:
        return 'Zero Only'
    
    def convert_hundred(n):
        if n == 0:
            return ''
        result = ''
        if n >= 100:
            result += ones[n // 100] + ' Hundred '
            n %= 100
        if n >= 20:
            result += tens[n // 10] + ' '
            n %= 10
        if n > 0:
            result += ones[n] + ' '
        return result.strip()
    
    result = ''
    
    if num >= 10000000:
        result += convert_hundred(num // 10000000) + ' Crore '
        num %= 10000000
    if num >= 100000:
        result += convert_hundred(num // 100000) + ' Lakh '
        num %= 100000
    if num >= 1000:
        result += convert_hundred(num // 1000) + ' Thousand '
        num %= 1000
    if num > 0:
        result += convert_hundred(num)
    
    return result.strip() + ' Only'

