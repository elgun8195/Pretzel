using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Pretzel_Backend.DAL;
using Pretzel_Backend.Extensions;
using Pretzel_Backend.Helpers;
using Pretzel_Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pretzel_Backend.Areas.AdminF.Controllers
{
    [Area("adminf")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Slider> sliders = _context.Sliders.ToList();
            return View(sliders);
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Slider db = _context.Sliders.Find(id);
            if (db == null) return NotFound();
            Helper.DeleteImage(_env, "images", db.Image);
            Helper.DeleteImage(_env, "images", db.Logo);
            _context.Sliders.Remove(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider Slider)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Slider.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            if (ModelState["PhotoLogo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Slider.PhotoLogo.IsImage())
            {
                ModelState.AddModelError("PhotoLogo", "Sekil Formati secin");
            }

            if (Slider.PhotoLogo.CheckSize(20000))
            {
                ModelState.AddModelError("PhotoLogo", "Sekil 20 mb-dan boyuk ola bilmez");
            }


            Slider db = new Slider();
            string filename1 = await Slider.Photo.SaveImage(_env, "images");
            string filename2 = await Slider.PhotoLogo.SaveImage(_env, "images");

            db.Image = filename1;
            db.Logo = filename2;


            _context.Sliders.Add(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }





        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider Slider = _context.Sliders.Find(id);
            if (Slider == null)
            {
                return NotFound();
            }
            return View(Slider);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Slider Slider)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (Slider.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            if (ModelState["PhotoLogo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!Slider.PhotoLogo.IsImage())
            {
                ModelState.AddModelError("PhotoLogo", "Sekil Formati secin");
            }

            if (Slider.PhotoLogo.CheckSize(20000))
            {
                ModelState.AddModelError("PhotoLogo", "Sekil 20 mb-dan boyuk ola bilmez");
            }



            Slider db = _context.Sliders.Find(id);
            if (db == null)
            {
                return NotFound();
            } 

            string filename1 = await Slider.Photo.SaveImage(_env, "images");
            string filename2 = await Slider.PhotoLogo.SaveImage(_env, "images");

            db.Image = filename1;
            db.Logo = filename2;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider slider = _context.Sliders.Find(id);
            if (slider == null)
            {
                return NotFound();
            }
            return View(slider);
        }


    }
}
