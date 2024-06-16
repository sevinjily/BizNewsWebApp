using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        [Authorize(Roles ="Admin,Super Admin")]
        public IActionResult Index()
        {
            var roles= _roleManager.Roles.ToList();
            return View(roles);
        }
        [HttpGet]
            public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var checkRole= await _roleManager.FindByNameAsync(role.Name);
            if(checkRole != null)
            {
                ViewData["Error"] = "This role name is already exists!";
                return View();
            }
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
    }
}
