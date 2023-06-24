using Microsoft.AspNetCore.Mvc;
using PustokMVC.Areas.Manage.ViewModels;
using PustokMVC.DAL;
using PustokMVC.Helpers;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokDbContext context ,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {

            var query = _context.Sliders.AsQueryable();

            return View(PaginatedList<Slider>.Create(query,page,3));
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {

            if (!ModelState.IsValid) return View();

            if (slider.FileImage.Length > 2*1024*1024)
            {
                ModelState.AddModelError("FileImage", "File max size 2 mb");
                return View();
            }

            if (slider.FileImage.ContentType!="image/jpeg" && slider.FileImage.ContentType!="image/png")
            {
                ModelState.AddModelError("FileImage", "File must be .jpeg, .jpg or .png");
                return View();
            }


            slider.Image = FileManager.Save(slider.FileImage, _env.WebRootPath, "~/manage/uploads/sliders");


            _context.Sliders.Add(slider);

            return RedirectToAction("index");
        }
    }
}
