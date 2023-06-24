using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Areas.Manage.ViewModels;
using PustokMVC.DAL;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class GenreController : Controller
    {
        private readonly PustokDbContext _context;

        public GenreController(PustokDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var query = _context.Genres.Include(x => x.Books);
           
            return View(PaginatedList<Genre>.Create(query,page,3));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {

            if (!ModelState.IsValid) return View();

            if (_context.Genres.Any(x => x.Name == genre.Name))           
            {
                ModelState.AddModelError("Name","Name has already beenn");

                return View();
            }

           

            _context.Genres.Add(genre);

            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var genre = _context.Genres.FirstOrDefault(x => x.Id == id);
            if (genre==null) return View("Error");


         
            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
            if (!ModelState.IsValid) return View();

            var existGenre = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);

            if (existGenre == null) return View("Error");

            if (existGenre.Name != genre.Name && _context.Genres.Any(x=>x.Name==genre.Name))
            {
                ModelState.AddModelError("Name", "Name has already been token");
                return View();
            }

            existGenre.Name = genre.Name;

            _context.SaveChanges();

            return RedirectToAction("index");
        }
    }


}
