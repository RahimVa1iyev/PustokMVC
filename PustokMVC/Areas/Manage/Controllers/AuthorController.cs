using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult Index()
        {
            List<Author> authors = _context.Authors.Include(x=>x.Books).ToList();

            return View(authors);
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

            Author author = _context.Authors.FirstOrDefault(x =>x.Id == id);

            return View(author);

        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Authors.FirstOrDefault(x=>x.FullName==author.FullName)!=null)
            {
                return View();
            }

            var existAuthor = _context.Authors.FirstOrDefault(x => x.Id == author.Id);
            existAuthor.FullName=author.FullName;

            _context.SaveChanges();

            return RedirectToAction("index");

        }
    }
}
