using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PustokMVC.DAL;
using PustokMVC.Models;
using PustokMVC.ModelView;
using System.Security.Claims;

namespace PustokMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public OrderController(PustokDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult CheckOut()
        {
            CheckOutVM vm = new CheckOutVM();
            vm.Orders = new OrderCreateVM();
            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
            vm.Items = _generatedCheckOutItems(userId);
            vm.TotalAmount = vm.Items.Sum(x => x.Price);

            if (userId != null)
            {
               
               

                var user = _userManager.FindByIdAsync(userId).Result;

                vm.Orders.FullName = user.FullName;
                vm.Orders.Email = user.Email;
                vm.Orders.Phone = user.PhoneNumber;

            }
           

            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(OrderCreateVM orderForm)
        {
            var userId = User.Identity.IsAuthenticated ? User.FindFirstValue(ClaimTypes.NameIdentifier) : null;
            Order order = new Order()
            {
                FullName = orderForm.FullName,
                Address = orderForm.Address,
                Email = orderForm.Email,
                Phone = orderForm.Phone,
                CreatedAt = DateTime.UtcNow.AddHours(4),
                Status = Enums.OrderStatus.Pending,
                Note = orderForm.Note,
                AppUserId = userId,
                OrderItems = _generatedOrderItems(userId),
                

            };
          
            order.TotalAmount = order.OrderItems.Sum(x => x.Count*(x.DiscountPercent>0? (x.UnitSalePrice*((100-x.DiscountPercent)/100)) : x.UnitSalePrice));


            _context.Orders.Add(order);
            _context.SaveChanges();

            if (userId !=null )
            {
                return RedirectToAction("profile", "account" , new {tab="Orders"});

            }

            return RedirectToAction("index","home");
        }

        private List<OrderItem> _generatedOrderItems(string userId = null)
        {
            List<OrderItem> items = new List<OrderItem>();

            if (userId != null)
            {
                var basketItems = _context.BasketItems.Include(x => x.Book).Where(x => x.UserId == userId).ToList();

                items = basketItems.Select(x => new OrderItem
                {
                    BookId = x.BookId,
                    UnitCostPrice = x.Book.CostPrice,
                    UnitSalePrice = x.Book.SalePrice,
                    DiscountPercent = x.Book.DiscountPercent,
                    Count = x.Count
                }).ToList();

            }
            else
            {
                var basketStr = HttpContext.Request.Cookies["basket"];

                if (basketStr != null)
                {

                    var basketItems = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);

                    foreach (var bi in basketItems)
                    {
                        var book = _context.Books.FirstOrDefault(x => x.Id == bi.BookId);

                        OrderItem orderItem = new OrderItem()
                        {
                            BookId = bi.BookId,
                            UnitCostPrice = book.CostPrice,
                            UnitSalePrice = book.SalePrice,
                            DiscountPercent = book.DiscountPercent,

                        };

                        items.Add(orderItem);

                    }

                }
            }


            return items;

        }

        private List<ItemCheckOutVM> _generatedCheckOutItems(string userId = null)
        {
            List<ItemCheckOutVM> items = new List<ItemCheckOutVM>();

            if (userId != null)
            {
                var basketItems = _context.BasketItems.Include(x => x.Book).Where(x => x.UserId == userId);


                items = basketItems.Select(x => new ItemCheckOutVM
                {
                    Count = x.Count,
                    BookName = x.Book.Name,
                    Price = x.Book.DiscountPercent > 0 ? x.Count * (x.Book.SalePrice * ((100 - x.Book.DiscountPercent) / 100)) : x.Count * x.Book.SalePrice,

                }).ToList();



            }
            else
            {
                var datastr = HttpContext.Request.Cookies["basket"];

                List<BasketCookieItemVM> basketCookies = null;

                if (datastr != null)
                {
                    basketCookies = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(datastr);
                }
                else
                {
                    basketCookies = new List<BasketCookieItemVM>();
                }

                foreach (var item in basketCookies)
                {

                    var book = _context.Books.FirstOrDefault(x => x.Id == item.BookId);

                    var basketcookie = new ItemCheckOutVM
                    {
                        Count = item.Count,
                        BookName = book.Name,
                        Price = book.DiscountPercent > 0 ? item.Count * (book.SalePrice * ((100 - book.DiscountPercent) / 100)) : item.Count * book.SalePrice,


                    };

                    items.Add(basketcookie);


                }

            }

            return items;
        }
    }
}
