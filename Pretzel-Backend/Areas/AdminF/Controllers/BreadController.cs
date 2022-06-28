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
    public class BreadController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BreadController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            List<Bread> breads = _context.Breads.ToList();
            return View(breads);
        }

        public IActionResult Delete(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }

            Bread db= _context.Breads.Find(id);
            if (db==null) return NotFound();
            Helper.DeleteImage(_env,"images",db.Image);
            _context.Breads.Remove(db);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public  async Task<IActionResult> Create(Bread bread)
        {
            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
                return View();
            }

            if (!bread.Photo.IsImage())
            {
                ModelState.AddModelError("Photo","Sekil Formati secin");
            }

            if (bread.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }


            Bread db= new Bread();
            string filename=await bread.Photo.SaveImage(_env,"images");
            db.Image = filename;
            db.Name = bread.Name;
            db.Desc = bread.Desc;

            _context.Breads.Add(db);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Bread bread = _context.Breads.Find(id);
            if (bread == null)
            {
                return NotFound();
            }
            return View(bread);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id,Bread bread)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (ModelState["Photo"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            {
               return View();
            }

            if (!bread.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Sekil Formati secin");
            }

            if (bread.Photo.CheckSize(20000))
            {
                ModelState.AddModelError("Photo", "Sekil 20 mb-dan boyuk ola bilmez");
            }
            Bread db = _context.Breads.Find(id);
            if (db == null)
            {
                return NotFound();
            }
            string filename =await bread.Photo.SaveImage(_env, "images");
            Bread existName = _context.Breads.FirstOrDefault(x=>x.Name.ToLower()==bread.Name.ToLower());

            if (existName != null)
            {
                if (db!=existName)
                {
                    ModelState.AddModelError("Name", "Name Already Exist");
                    return View();
                }
            }

            db.Image =filename;
            db.Name = bread.Name;
            db.Desc = bread.Desc;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }


        public IActionResult Detail(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Bread bread = _context.Breads.Find(id);
            if (bread==null)
            {
                return NotFound();
            }
            return View(bread);
        }

    }
}
