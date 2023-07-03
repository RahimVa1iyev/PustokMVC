using PustokMVC.Models;

namespace PustokMVC.ModelView
{
    public class ProfileVM
    {
        public MemberProfileUpdateVM  member { get; set; }

        public List<Order> Orders { get; set; }
    }
}
