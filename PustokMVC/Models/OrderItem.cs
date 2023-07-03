using System.ComponentModel.DataAnnotations.Schema;

namespace PustokMVC.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int BookId { get; set; }

        public int Count { get; set; }

        [Column(TypeName="decimal(10,2)")]
        public decimal UnitSalePrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]

        public decimal UnitCostPrice { get; set; }

        [Column(TypeName = "decimal(10,2)")]

        public decimal DiscountPercent { get; set; }

        public Order Order { get; set; }

        public Book Book { get; set; }

    }
}
