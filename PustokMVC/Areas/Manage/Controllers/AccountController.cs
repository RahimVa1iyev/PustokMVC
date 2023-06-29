using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async  Task<IActionResult> CreateAdmin()
        {

            AppUser admin = new()
            {
                FullName = "SuperAdmin",
                UserName = "superadmin",
            };

            var result = await _userManager.CreateAsync(admin,"admin123");

            return Content("yaradildi");


        }


        public IActionResult Login()
        {

            return View();
        }
    }
}
