using PustokMVC.Models;

namespace PustokMVC.ModelView
{
    public class HomeModelView
    {

        public List<Slider> Sliders { get; set; }

        public List<Book> Books { get; set; }

        public List<Book> FeaturedBooks { get; set; }

        public List<Book> NewBooks { get; set; }

        public List<Book>  DiscountedBooks { get; set; }

        public List<HomeFeature> HomeFeatures { get; set; }

        public List<Promotion> Promotions { get; set; }
    }
}
