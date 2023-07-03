using PustokMVC.Enums;
using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string AppUserId { get; set; }
        [Required]
        [MaxLength(35)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(85)]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
        public string Phone { get; set; }

        [Required]
        [MaxLength(35)]
        public string Address { get; set; }

        public decimal TotalAmount { get; set; }
      
        [MaxLength(200)]
        public string Note { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatus Status { get; set; }

        public List<OrderItem> OrderItems { get; set; }

    }
}
