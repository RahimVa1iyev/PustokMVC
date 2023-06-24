using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
