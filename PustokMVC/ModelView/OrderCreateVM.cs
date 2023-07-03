using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ModelView
{
    public class OrderCreateVM
    {

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


        [MaxLength(200)]
        public string Note { get; set; }
    }
}
