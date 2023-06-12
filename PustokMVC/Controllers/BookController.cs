using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.DAL;
using PustokMVC.Models;

namespace PustokMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly PustokDbContext _context;

        public BookController(PustokDbContext context)
        {
            this._context = context;
        }

        public IActionResult Detail(int id)
        {
            Book? book = _context.Books
                            .Include(b => b.Author)
                            .Include(b => b.Genre)
                            .Include(b => b.BookTags)
                            .ThenInclude(bt => bt.Tag)
                            .Include(b => b.Images.Where(i => i.ImageStatus == true))
                            .FirstOrDefault(x => x.Id == id);
            return PartialView("_ModalPartial",book);
        }
    }
}
