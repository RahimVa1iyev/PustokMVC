using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ModelView
{
    public class MemberRegisterVM
    {
        [Required]
        [MaxLength(25)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(25)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(60)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(15)]
       
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(25)]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Required]
        [MaxLength(25)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]

        public string ConfirmedPassword { get; set; }



    }
}
