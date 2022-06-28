using Microsoft.AspNetCore.Mvc;
using Pretzel_Backend.DAL;
using Pretzel_Backend.ViewModels;
using System.Linq;

namespace Pretzel_Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeVM homeVM=new HomeVM();
            homeVM.Slider = _context.Sliders.ToList();
            homeVM.Bread=_context.Breads.ToList();
            return View(homeVM);
        }
    }
}
