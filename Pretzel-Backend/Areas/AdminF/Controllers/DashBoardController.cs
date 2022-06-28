using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pretzel_Backend.Areas.AdminF.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("AdminF")]
        [Authorize(Roles ="Admin,SuperAdmin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
