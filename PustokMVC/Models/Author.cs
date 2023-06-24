using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMVC.Models
{
    public class Author
    {
        public int Id { get; set; }

        [StringLength(40)]
        [Required]
        public string FullName { get; set; }

        public List<Book> Books { get; set; }
    }
}
