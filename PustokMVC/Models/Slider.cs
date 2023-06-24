using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMVC.Models
{
    public class Slider
    {
        public int Id { get; set; }

        public string FirstTitle { get; set; }

        public string SecondTitle { get; set; }

        
        public string? Description { get; set; }

        public string ButtonText { get; set; }

        public string ButtonUrl { get; set; }

        public string Image { get; set; }

        [NotMapped]

        public IFormFile FileImage { get; set; }
    }
}
