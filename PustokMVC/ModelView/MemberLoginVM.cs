using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ModelView
{
    public class MemberLoginVM
    {
        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
