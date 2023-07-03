using System.ComponentModel.DataAnnotations;

namespace PustokMVC.ModelView
{
    public class MemberProfileUpdateVM
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

        [MaxLength(25)]
        [DataType(DataType.Password)]

        public string CurrentPassword { get; set; }

        [MaxLength(25)]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }

        [MaxLength(25)]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword))]

        public string ConfirmedPassword { get; set; }


    }
}
