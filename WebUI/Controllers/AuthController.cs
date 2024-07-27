using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.DTOs;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor contextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]   
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if(!ModelState.IsValid) {
            return View();
            }
            User newUser = new()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                PhotoUrl = "/",
                UserName=registerDTO.Email
            };
            IdentityResult  identityResult= await _userManager.CreateAsync(newUser,registerDTO.Password);
            if(identityResult.Succeeded)
            {
                               return RedirectToAction("Index","Home");
            }
            else
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError("error", error.Description);
                }
                return View();
            }
           
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
           if(!ModelState.IsValid)
            {
                return View();
            }
           var findUser= await _userManager.FindByEmailAsync(loginDTO.Email);
            if(findUser == null)
            {
                ModelState.AddModelError("error", "İstifadəçi tapılmadı!");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult signInResult= await _signInManager.PasswordSignInAsync(findUser, loginDTO.Password,loginDTO.RememberMe,false);
            if(signInResult.Succeeded)
            {
                var c = _contextAccessor.HttpContext.Request.Query["controller"];
                var a = _contextAccessor.HttpContext.Request.Query["action"];
                var id = _contextAccessor.HttpContext.Request.Query["id"];
                var seoUrl = _contextAccessor.HttpContext.Request.Query["seoUrl"];
                if (!string.IsNullOrEmpty(c))
                {
                    return RedirectToAction(a, c, new { Id = id, SeoUrl = seoUrl });

                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("error", "İstifadəçi adı və ya parol yanlışdır");
                return View();
            }
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
