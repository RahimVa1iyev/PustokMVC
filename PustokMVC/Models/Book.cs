using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMVC.Models
{
    public class Book
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public int GenreId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public bool StockStatus { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal SalePrice { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal CostPrice { get; set; }
        [Range(1,100)]
        public decimal  DiscountPercent { get; set; }

        [StringLength(600)]
        public string Description { get; set; }

        [Required]
        public bool IsNew { get; set; }

        [Required]
        public bool IsFetured { get; set; }

        public ICollection<Image> Images { get; set; }

        public ICollection<BookTag> BookTags { get; set; }

        public Author Author { get; set; }

        public Genre Genre { get; set; }
    }
}
