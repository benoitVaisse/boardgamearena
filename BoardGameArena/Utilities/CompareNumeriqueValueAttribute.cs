using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGameArena.Utilities
{
    public class CompareNumeriqueValueAttribute : ValidationAttribute
    {
        private readonly string anotherProperty;
        private readonly string methode;
        private bool result;

        public CompareNumeriqueValueAttribute(string anotherProperty, string methode)
        {
            this.anotherProperty = anotherProperty;
            this.methode = methode;
        }

        protected override ValidationResult IsValid(object value, ValidationContext ValidationContext)
        {
            var Value = value.ToString();
            var property = ValidationContext.ObjectType.GetProperty(this.anotherProperty);
            var propertyValue = property.GetValue(ValidationContext.ObjectInstance, null).ToString();

            int theValue = Int32.Parse(Value);
            int ThePropertyValue = Int32.Parse(propertyValue);

           if (this.methode == "<")
            {
                this.result =  theValue < ThePropertyValue;
            }
            else if (this.methode == "<=")
            {
                this.result = theValue <= ThePropertyValue;
            }
            else if (this.methode == ">")
            {
                this.result = theValue > ThePropertyValue;
            }
            else if (this.methode == ">=")
            {
                this.result = theValue >= ThePropertyValue;
            }
            else if (this.methode == "==")
            {
                this.result = theValue == ThePropertyValue;
            }

            if (this.result == false)
            {
                return new ValidationResult(string.Format(
                    CultureInfo.CurrentCulture,
                    FormatErrorMessage(ValidationContext.DisplayName),
                    new[] { this.anotherProperty }
                ));

                
            }
            return ValidationResult.Success;
        }
    }
}
