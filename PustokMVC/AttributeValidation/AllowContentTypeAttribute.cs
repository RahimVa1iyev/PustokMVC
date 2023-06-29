using System.ComponentModel.DataAnnotations;

namespace PustokMVC.AttributeValidation
{
    public class AllowContentTypeAttribute: ValidationAttribute
    {
        private readonly string[] _contents;

        public AllowContentTypeAttribute(params string[] contents)
        {
            _contents = contents;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            List<IFormFile> list = new List<IFormFile>();

            if (value is IFormFile file)
            {
                list.Add(file);
            }
            else if (value is List<IFormFile> files)
            {
                list.AddRange(files);
            }

            foreach (var item in list)
            {

                if (!_contents.Contains(item.ContentType))
                {
                    return new ValidationResult($"File content type must be one of them {string.Join(',', _contents)}");
                }

            }


            return ValidationResult.Success;
        }
    }
}
