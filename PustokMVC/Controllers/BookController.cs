using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokMVC.DAL;
using PustokMVC.Models;
using PustokMVC.ModelView;

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

        public IActionResult SetProducts(int id)
        {
            var basketItem = HttpContext.Request.Cookies["basket"];

            List<BasketCookieItemVM> items = null;

            if (basketItem != null)
            {
                items = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketItem);
            }
            else
            {
                items = new List<BasketCookieItemVM>();
            }

            BasketCookieItemVM item = items.FirstOrDefault(x => x.BookId == id);

            if (item == null)
            {
                item = new()
                {
                    BookId = id,
                    Count = 1
                };

                items.Add(item);
            }
            else
                item.Count++;
          


            HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(items));

            return RedirectToAction("index","home");

        }

        public IActionResult GetProducts()
        {
            var dataStr = HttpContext.Request.Cookies["basket"];
            var data = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(dataStr);
            return Json(data);
        }
    }
}
