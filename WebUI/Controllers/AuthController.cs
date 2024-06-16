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

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize(Roles ="Admin")]

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
