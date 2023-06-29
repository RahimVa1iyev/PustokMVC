using PustokMVC.AttributeValidation;
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

        public bool StockStatus { get; set; }

        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal SalePrice { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal CostPrice { get; set; }
        [Range(1,100)]
        public decimal  DiscountPercent { get; set; }

        [StringLength(600)]
        public string Description { get; set; }

        public bool IsNew { get; set; }

        public bool IsFetured { get; set; }

        [NotMapped]
        [FileMaxLength(2097152)]
        [AllowContentType("image/jpeg" , "image/png")]
        public IFormFile PosterFile { get; set; }

        [NotMapped]
        [FileMaxLength(2097152)]
        [AllowContentType("image/jpeg", "image/png")]

        public IFormFile HoverFile { get; set; }

        [NotMapped]
        [AllowContentType("image/jpeg", "image/png")]

        public List<IFormFile> AllFiles { get; set; } = new List<IFormFile>();
        [NotMapped]
        public List<int> TagIds { get; set; } = new List<int>();

        [NotMapped]
        public List<int> ImageIds { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();

        public List<BookTag> BookTags { get; set; } = new List<BookTag>();

        public Author Author { get; set; }

        public Genre Genre { get; set; }
    }
}
