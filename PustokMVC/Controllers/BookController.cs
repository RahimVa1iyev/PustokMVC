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
            Book book = _context.Books
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

            BasketVM basketVM = new BasketVM();

            foreach (var cookieItem in items)
            {
                BasketItemVM basketItemVM = new()
                {
                    Book = _context.Books.Include(x => x.Images.Where(x => x.ImageStatus == true)).FirstOrDefault(x => x.Id == id),
                    Count = cookieItem.Count,
                };

                basketVM.BasketItemVMs.Add(basketItemVM);
                basketVM.TotalAmount += cookieItem.Count * (basketItemVM.Book.DiscountPercent > 0 ? (basketItemVM.Book.SalePrice * ((100 - basketItemVM.Book.DiscountPercent) / 100)) : basketItemVM.Book.SalePrice);

            }


            return RedirectToAction("index","home");

        }

        public IActionResult DeleteProducts(int id)
        {
            var basketStr = HttpContext.Request.Cookies["basket"];

            List<BasketCookieItemVM> cookieItems = null;

            if (basketStr != null)
                cookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);
            else
            {
                cookieItems = new List<BasketCookieItemVM>();
            }

            var existProduct = cookieItems.FirstOrDefault(x => x.BookId == id);

            if (existProduct != null)
            {
                cookieItems.Remove(existProduct);
            }
            
            HttpContext.Response.Cookies.Append("basket",JsonConvert.SerializeObject(cookieItems));



            return RedirectToAction("index","home");
        }
    }
}
