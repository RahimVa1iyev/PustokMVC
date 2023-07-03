using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokMVC.DAL;
using PustokMVC.Models;
using PustokMVC.ModelView;

namespace PustokMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly PustokDbContext _context;

        public AccountController(UserManager<AppUser> userManager , SignInManager<AppUser> signInManager , PustokDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Register(MemberRegisterVM registerVM)
        {

            if (!ModelState.IsValid) return View();

            AppUser user = new()
            {
                FullName = registerVM.FullName,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber
            };

           var result = await _userManager.CreateAsync(user, registerVM.Password);

            await _userManager.AddToRoleAsync(user, "Member");

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    return View();

                }
            }


            return RedirectToAction("login");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginVM loginVM)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser user = await _userManager.FindByNameAsync(loginVM.UserName);

            if (user==null)
            {
                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password,false,false);

            if (!result.Succeeded)
            {

                ModelState.AddModelError("", "Username or Password is incorrect");
                return View();

            }



            return RedirectToAction("index", "home");

        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");

              
        }

        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Profile( string tab = "Profile")
        {
            ViewBag.Tab = tab;
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            ProfileVM vm = new ProfileVM()
            {
                member = new()
                {
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber
                },
                Orders = _context.Orders.Include(x => x.OrderItems).Where(x => x.AppUserId == user.Id).ToList(),
                
            };

         return View(vm);        

        }

        public async Task<IActionResult> MemberUpdate(MemberProfileUpdateVM memberProfile)
        {
            if (!ModelState.IsValid)
            {
                ProfileVM vm = new() { member = memberProfile };
                return View("Profile",vm);
            }

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            user.FullName = memberProfile.FullName;
            user.UserName = memberProfile.UserName;
            user.Email = memberProfile.Email;
            user.PhoneNumber = memberProfile.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                    ProfileVM vm = new() { member = memberProfile };
                    return View("Profile", vm);

                }
            }

            await _signInManager.SignInAsync(user,false);



            return RedirectToAction("profile");
        }
    }
}
