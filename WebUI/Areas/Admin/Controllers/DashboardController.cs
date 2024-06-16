using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
        [Area(nameof(Admin))]
    public class DashboardController : Controller
    {
        [Authorize(Roles ="Admin,Super Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
