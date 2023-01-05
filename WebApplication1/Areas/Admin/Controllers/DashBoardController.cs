using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles ="Admin,Moderator")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
