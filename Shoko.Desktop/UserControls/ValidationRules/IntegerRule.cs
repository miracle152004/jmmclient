﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace Shoko.Desktop.UserControls.ValidationRules
{
    class IntegerRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                if (((string)value).Length > 0 && !int.TryParse((String)value, out int parameter))
                        return new ValidationResult(false, "Must be an integer");
            }
            catch (Exception)
            {
                return new ValidationResult(false, "Must be an integer");
            }

            return new ValidationResult(true, null);
        }
    }
}
