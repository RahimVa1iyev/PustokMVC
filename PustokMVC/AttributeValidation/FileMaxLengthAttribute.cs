using System.ComponentModel.DataAnnotations;

namespace PustokMVC.AttributeValidation
{
    public class FileMaxLengthAttribute : ValidationAttribute
    {
        private readonly int _maxLength;

        public FileMaxLengthAttribute(int maxlength)
        {
           _maxLength = maxlength;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile)
            {
                var file = (IFormFile)value;

                if (file.Length > 2 * 1024 * 1024)
                {
                    return new ValidationResult($"FileLength must be less or equal than {_maxLength / 1024 / 1024} MB");
                }

            }

            return ValidationResult.Success;
        }
    }
}
