using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PustokMVC.Areas.Manage.ViewModels;
using PustokMVC.DAL;
using PustokMVC.Helpers;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    [Authorize("Admin,SuperAdmin")]
    [Area("manage")]
    public class SliderController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {

            var query = _context.Sliders.AsQueryable();

            return View(PaginatedList<Slider>.Create(query, page, 3));
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {

            if (!ModelState.IsValid) return View();

        

            if (slider.FileImage.ContentType != "image/jpeg" && slider.FileImage.ContentType != "image/png")
            {
                ModelState.AddModelError("FileImage", "File must be .jpeg, .jpg or .png");
                return View();
            }


            slider.Image = FileManager.Save(slider.FileImage, _env.WebRootPath, "/manage/uploads/sliders/");


            _context.Sliders.Add(slider);

            _context.SaveChanges();

            return RedirectToAction("index");
        }


        public IActionResult Edit(int id)
        {
            Slider slider = _context.Sliders.Find(id);
            if (slider == null) return View("Error");
            return View(slider);
        }

        [HttpPost]
        public IActionResult Edit(Slider slider)
        {
            if (!ModelState.IsValid) return View(slider);

            Slider existSlider = _context.Sliders.Find(slider.Id);

            string removebleImage = null;

            if (existSlider == null) return View("Error");

            if (slider.FileImage != null)
            {
              
                if (slider.FileImage.ContentType != "image/jpeg" && slider.FileImage.ContentType != "image/png")
                {
                    return View(slider);
                }

                removebleImage = existSlider.Image;
                existSlider.Image = FileManager.Save(slider.FileImage, _env.WebRootPath, "/manage/uploads/sliders/");
            }

            existSlider.FirstTitle = slider.FirstTitle;
            existSlider.SecondTitle = slider.SecondTitle;
            existSlider.ButtonText = slider.ButtonText;
            existSlider.ButtonUrl = slider.ButtonUrl;
            existSlider.Description = slider.Description;

            _context.SaveChanges();

            if (removebleImage!=null)
            {
                FileManager.Delete(_env.WebRootPath, "manage/uploads/sliders", removebleImage);
            }

            return RedirectToAction("index");
        }
    }
}
