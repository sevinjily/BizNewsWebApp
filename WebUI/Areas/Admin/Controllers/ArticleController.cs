using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly HttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        public ArticleController(AppDbContext context, IWebHostEnvironment env, HttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _env = env;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var articles = _context.Articles.ToList();
            return View(articles);
        }
        [HttpGet]

       public IActionResult Create()
        {
            var categories=_context.Categories.ToList();
            var tags=_context.Tags.ToList();
            ViewData["tags"] = tags;   
            ViewBag.Categories = new SelectList(categories,"Id","CategoryName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Article article,IFormFile file,List<Tag> tagIds)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user=await _userManager.FindByIdAsync(userId);
            var path = Path.Combine("/uploads/", Guid.NewGuid() + file.FileName);
            //var path="uploads/"+Guid.NewGuid()+ file.FileName;
            using FileStream fileStream = new(_env.WebRootPath + path,FileMode.Create);
            await file.CopyToAsync(fileStream);
            Article newArticle = new();
            newArticle.PhotoUrl = article.PhotoUrl;
            newArticle.Title = article.Title;
            newArticle.Content= article.Content;
            newArticle.CreatedDate = DateTime.Now;
            newArticle.CategoryId=article.CategoryId;
            newArticle.IsActive = article.IsActive;
            newArticle.IsFeatured = article.IsFeatured;
            newArticle.CreatedBy = user.FirstName+ " "+user.LastName;

            _context.Articles.Add(newArticle);
            _context.SaveChanges();

            return View();
        }
        
    }
}
