using System;
using System.Globalization;
using System.Windows.Controls;

namespace DAN_XLIII.Validation
{
    class ValidPositiveNumber : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string number = value as string;
            if(int.TryParse(number, out int n))
            {
                if (n < 0)
                {
                    return new ValidationResult(false, "Enter positive number");
                }
                else
                {
                    return new ValidationResult(true, null);
                }
            }
            else
            {
                return new ValidationResult(false, "Enter positive number");
            }
        }
    }
}
