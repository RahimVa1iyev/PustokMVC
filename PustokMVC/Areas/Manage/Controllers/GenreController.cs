using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.DAL;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GenreController : Controller
    {
        private readonly PustokDbContext _context;

        public GenreController(PustokDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Genre> genres = _context.Genres.Include(x=>x.Books).ToList();
            return View(genres);
        }
        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if(!ModelState.IsValid)
                return View();
            if (_context.Genres.Any(x=>x.Name==genre.Name))
            {
                ModelState.AddModelError("Name", "Name has already been ");

                return View();
            }
            _context.Genres.Add(genre);

            _context.SaveChanges();

            return RedirectToAction("index","genre");
        }

        public IActionResult Edit(int id)
        {
            if (!_context.Genres.Any(x=>x.Id==id))
            {
                ModelState.AddModelError("Id", "Genre not found");
                return View();
            }

            Genre existGenre = _context.Genres.FirstOrDefault(x=>x.Id==id);

            return View(existGenre);
        }

        [HttpPost]
        public IActionResult Edit(Genre genre)
        {
           

            if (_context.Genres.Any(x => x.Name == genre.Name))
            {
                ModelState.AddModelError("Name", "Name has already been");

                return View();

            }

            Genre existGenre = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);

            existGenre.Name = genre.Name;

            _context.SaveChanges();


            return View("index","genre");
        }
    }
}
