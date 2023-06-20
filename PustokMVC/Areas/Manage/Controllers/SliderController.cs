using Microsoft.AspNetCore.Mvc;
using PustokMVC.DAL;
using PustokMVC.Helpers;
using PustokMVC.Models;

namespace PustokMVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly PustokDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(PustokDbContext context,IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Slider slider)
        {
            if(!ModelState.IsValid ) return View();

            if (slider.FormFile==null)
            {
                ModelState.AddModelError("FormFile", "File is required");
                return View();
            }

            if (slider.FormFile.Length > 2* 1024 * 1024)
            {
                ModelState.AddModelError("FormFile" ,"File max size must be 2mb");
                return View();
            }

            if (slider.FormFile.ContentType!="image/jpeg" && slider.FormFile.ContentType!="image/png")
            {
                ModelState.AddModelError("FormFile", "File must be .jpeg, .jpg or .png ");
                return View();
            }


            

            slider.Image = FileManager.Save(slider.FormFile, _env.WebRootPath, "image/bg-images"); 

            _context.Sliders.Add(slider);

            _context.SaveChanges();

            return RedirectToAction("index");


        }
    }
}
