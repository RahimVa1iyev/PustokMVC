using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokMVC.DAL;
using PustokMVC.ModelView;

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
                basketVM.TotalAmount += cookieItem.Count * (basketItem.Book.DiscountPercent > 0 ? (basketItem.Book.SalePrice * ((100 - basketItem.Book.DiscountPercent) / 100)) : basketItem.Book.SalePrice );
            }


            return basketVM;

        }
    }
}
