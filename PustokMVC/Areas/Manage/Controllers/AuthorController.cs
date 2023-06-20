using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Areas.Manage.ViewModels;
using PustokMVC.DAL;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthorController : Controller
    {
        private readonly PustokDbContext _context;

        public AuthorController(PustokDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1, string search =null)
        {


            var query = _context.Authors.Include(x=>x.Books).AsQueryable();

            if(search!=null) query = query.Where(x=>x.FullName.Contains(search));
           

            return View(PaginatedList<Author>.Create(query,page,2));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Authors.Add(author);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {

            Author author = _context.Authors.Find(id);

            if (author == null) return View("error");

            return View(author);

        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existAuthor = _context.Authors.Find(author.Id);

            if (existAuthor == null) return View("error");

            existAuthor.FullName=author.FullName;

            _context.SaveChanges();

            return RedirectToAction("index");

        }
    }
}
