using System.ComponentModel.DataAnnotations;

namespace PustokMVC.Models
{
    public class HomeFeature
    {
        public int Id { get; set; }

        [Required]
        public string Icon { get; set; }

        [StringLength(60)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
    }
}
