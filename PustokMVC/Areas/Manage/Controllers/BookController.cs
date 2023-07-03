using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.Areas.Manage.ViewModels;
using PustokMVC.DAL;
using PustokMVC.Helpers;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    [Authorize("Admin,SuperAdmin")]

    [Area("manage")]
    public class BookController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(PustokDbContext context , IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page =1)
        {
            var books = _context.Books.Include(x => x.Author).Include(x => x.Genre).Include(x => x.Images.Where(x => x.ImageStatus == true));

            return View(PaginatedList<Book>.Create(books,page,4));
        }

        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            return View();
        }

        [HttpPost]

        public IActionResult Create(Book book)
        {
     

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                return View();
            }

            if (!_context.Genres.Any(x=>x.Id==book.GenreId))
            {
                return View("Error");
            }

            if (!_context.Authors.Any(x=>x.Id==book.AuthorId))
            {
                return View("Error");

            }
            
           

            Image posterImage = new()
            {
                ImageStatus = true,
                ImageName = FileManager.Save(book.PosterFile, _env.WebRootPath, "manage/uploads/books"),

            };
            book.Images.Add(posterImage);

            Image hoverImage = new()
            {
                ImageStatus = false,
                ImageName = FileManager.Save(book.HoverFile, _env.WebRootPath, "manage/uploads/books"),

            };
            book.Images.Add(hoverImage);

            foreach (var file in book.AllFiles)
            {
                Image all = new()
                {
                    ImageStatus = null,
                    ImageName = FileManager.Save(file, _env.WebRootPath, "manage/uploads/books"),
                };
                book.Images.Add(all);
            }

            _context.Books.Add(book);
            _context.SaveChanges();


            return RedirectToAction("index");
        }

        public IActionResult Edit(int id)
        {
            Book book = _context.Books.Include(x=>x.Images).Include(x=>x.BookTags).FirstOrDefault(x=>x.Id==id);

            if (book == null) return View("error");

            book.TagIds = book.BookTags.Select(x => x.TagId).ToList();

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags =  _context.Tags.ToList();
            

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            Book existBook = _context.Books.Include(x => x.Images).Include(x => x.BookTags).FirstOrDefault(x => x.Id == book.Id);

            if (existBook == null) return View("error");

            if (!_context.Authors.Any(x=>x.Id==book.AuthorId))
            {
                return View("Error");
            }
            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                return View("Error");
            }

            existBook.BookTags = new List<BookTag>();

            foreach (var tagId in book.TagIds)
            {
                if (!_context.Tags.Any(x=>x.Id==tagId))
                {
                    return View("error");
                }

                existBook.BookTags.Add(new BookTag { TagId=tagId });

            }
            List<string> removebleImages = new List<string>();

            if (book.PosterFile != null)
            {
                Image poster = existBook.Images.FirstOrDefault(x => x.ImageStatus == true);
                removebleImages.Add(poster.ImageName);
                poster.ImageName = FileManager.Save(book.PosterFile, _env.WebRootPath, "manage/uploads/books");
            }

            if (book.HoverFile != null)
            {
                Image hoverPoster = existBook.Images.FirstOrDefault(x => x.ImageStatus == false);
                removebleImages.Add(hoverPoster.ImageName);
                hoverPoster.ImageName = FileManager.Save(book.HoverFile, _env.WebRootPath, "manage/uploads/books");
            }

            var removableImageNames = existBook.Images.FindAll(x => x.ImageStatus == null && !book.ImageIds.Contains(x.Id));
            _context.Images.RemoveRange(removableImageNames);

            removebleImages.AddRange(removableImageNames.Select(x => x.ImageName).ToList());

            foreach (var file in book.AllFiles)
            {

                Image img = new()
                {
                    ImageStatus = null,
                    ImageName = FileManager.Save(file, _env.WebRootPath, "manage/uploads/books")
                };
                existBook.Images.Add(img);

            }

            existBook.StockStatus= book.StockStatus;
            existBook.CostPrice = book.CostPrice;
            existBook.DiscountPercent = book.DiscountPercent;
            existBook.SalePrice = book.SalePrice;
            existBook.Description = book.Description;
            existBook.IsNew = book.IsNew;
            existBook.IsFetured = book.IsFetured;
            existBook.Name = book.Name;
            existBook.AuthorId = book.AuthorId;
            existBook.GenreId = book.GenreId;


            _context.SaveChanges();


            FileManager.DeleteAll(_env.WebRootPath,"manage/uploads/books",removebleImages);

            return RedirectToAction("index");
        }
    }
}
