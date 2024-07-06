using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebUI.Data;
using WebUI.Helpers;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;
        public ArticleController(AppDbContext context, IWebHostEnvironment env, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _env = env;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var articles = _context.Articles
                .Where(x=>x.IsDeleted==false)
                .Include(x=>x.Category)
                .Include(x=>x.ArticleTags)
                .ThenInclude(x=>x.Tag)
                .ToList();
            return View(articles);
        }
        [Authorize]
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
            var categories = _context.Categories.ToList();
            var tags = _context.Tags.ToList();
            ViewData["tags"] = tags;
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

            var userId =_contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user=await _userManager.FindByIdAsync(userId);
            Article newArticle = new();

            if (file != null)
            {

            newArticle.PhotoUrl = await file.SaveFileAsync(_env.WebRootPath,"/article-images/");
            }
            else
            {
                return View();
            }
           
            newArticle.Title = article.Title;
            newArticle.Content= article.Content;
            newArticle.CreatedDate = DateTime.Now;
            newArticle.CategoryId=article.CategoryId;
            newArticle.IsActive = article.IsActive;
            newArticle.IsFeatured = article.IsFeatured;
            newArticle.CreatedBy = user.FirstName+ " " +user.LastName;
            newArticle.SeoUrl = "";

           await _context.Articles.AddAsync(newArticle);
           await _context.SaveChangesAsync();

            for (int i = 0; i < tagIds.Count; i++)
            {
                ArticleTag articleTag = new()
                {
                    ArticleId = newArticle.Id,
                    TagId = tagIds[i].Id
                };
              await _context.ArticleTags.AddAsync(articleTag);
                

            }
           await _context.SaveChangesAsync();

            return Redirect("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var article = _context.Articles.FirstOrDefault(x => x.Id == id);
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
            return View();
        }
        
    }
}
