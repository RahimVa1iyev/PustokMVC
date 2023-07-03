using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Areas.Manage.ViewModels;
using PustokMVC.DAL;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    [Authorize("Admin,SuperAdmin")]
    [Area("manage")]
    public class AuthorController : Controller
    {
        private readonly PustokDbContext _context;

        public AuthorController(PustokDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            var query = _context.Authors.Include(x=>x.Books);
          

            return View(PaginatedList<Author>.Create(query,page,3));
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid) return View();

            _context.Authors.Add(author);
            _context.SaveChanges();
          
            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Author author = _context.Authors.FirstOrDefault(x=>x.Id==id);

            if (author==null) return View("Error");

            return View(author);

        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (!ModelState.IsValid) return View();

           var existAuthor = _context.Authors.FirstOrDefault(x=>x.Id==author.Id);
            if (existAuthor==null) return View("Error");

            existAuthor.FullName = author.FullName;

            _context.SaveChanges();
          
            return RedirectToAction("index");
        }


    }
}
