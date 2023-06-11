using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.DAL;
using PustokMVC.ModelView;
using System.Diagnostics;
using System.Linq;

namespace PustokMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly PustokDbContext _context;

        public HomeController(PustokDbContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            HomeModelView vm = new()
            {
                Sliders = _context.Sliders.ToList(),
                HomeFeatures = _context.HomeFeatures.ToList(),
                NewBooks = _context.Books.Include(b=>b.Author).Include(b=>b.Images).Where(b=>b.IsNew==true).ToList(),
                FeaturedBooks = _context.Books.Include(b => b.Author).Include(b => b.Images).Where(b => b.IsFetured == true).ToList(),
                DiscountedBooks = _context.Books.Include(b => b.Author).Include(b => b.Images).Where(b => b.DiscountPercent>0).Take(5).ToList(),
                Promotions = _context.Promotions.ToList(),
            };

            return View(vm);
        }

    }
}