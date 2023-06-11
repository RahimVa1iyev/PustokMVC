using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMVC.Models
{
    public class Author
    {
        public int Id { get; set; }

        [StringLength(40)]
        public string FullName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
