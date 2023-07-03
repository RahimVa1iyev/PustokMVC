using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokMVC.DAL;
using PustokMVC.ModelView;
using System.Security.Claims;

namespace PustokMVC.Services
{
    public class LayoutService
    {
        private readonly PustokDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LayoutService(PustokDbContext context , IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public BasketVM GetBasket()
        {
            var basketVM = new BasketVM();

            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var items = _context.BasketItems.Include(x => x.Book).ThenInclude(x => x.Images.Where(x => x.ImageStatus == true)).Where(x => x.UserId == userId).ToList();

                foreach (var item in items)
                {
                    BasketItemVM basketItem = new()
                    {
                        Book = item.Book,
                        Count = item.Count,
                    };
                    basketVM.BasketItemVMs.Add(basketItem);
                    basketVM.TotalAmount += basketItem.Count * (basketItem.Book.DiscountPercent > 0 ? (basketItem.Book.SalePrice * ((100 - basketItem.Book.DiscountPercent) / 100)) : basketItem.Book.SalePrice);
                }
            }
            else
            {
                var dataStr = _httpContextAccessor.HttpContext.Request.Cookies["basket"];

                List<BasketCookieItemVM> cookieItems = null;

                if (dataStr == null)
                    cookieItems = new List<BasketCookieItemVM>();
                else
                    cookieItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(dataStr);
                basketVM.BasketItemVMs = new List<BasketItemVM>();
                foreach (var cookieItem in cookieItems)
                {
                    BasketItemVM basketItem = new()
                    {
                        Book = _context.Books.Include(x => x.Images.Where(x => x.ImageStatus == true)).FirstOrDefault(x => x.Id == cookieItem.BookId),
                        Count = cookieItem.Count,
                    };
                    basketVM.BasketItemVMs.Add(basketItem);
                    basketVM.TotalAmount += basketItem.Count * (basketItem.Book.DiscountPercent > 0 ? (basketItem.Book.SalePrice * ((100 - basketItem.Book.DiscountPercent) / 100)) : basketItem.Book.SalePrice);
                }
            }


            return basketVM;

        }
    }
}
