using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Image
    {
        public int Id { get; set; }


        public int BookId { get; set; }

        [Required]
        public string ImageName { get; set; }

        public  bool? ImageStatus { get; set; }

        public Book Book { get; set; }

    }
}
