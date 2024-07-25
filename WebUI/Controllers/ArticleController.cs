using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public ArticleController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Detail(Guid id)
        {
            var article = _context.Articles
            .Include(x => x.Category)
            .Include(x => x.ArticleTags)
            .ThenInclude(x => x.Tag)
            .FirstOrDefault(x => x.Id == id);
           

            var cookie = _contextAccessor.HttpContext.Request.Cookies["Views"];
            string[] findCookie = { "" };

            if (cookie != null)
            {
                findCookie = cookie.Split('/').ToArray();
            }

            if (!findCookie.Contains(article.Id.ToString()))
            {
                Response.Cookies.Append($"Views", $"{cookie}/{article.Id}", new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    Expires = DateTime.Now.AddSeconds(10)
                });
                article.ViewCount += 1;
                _context.Articles.Update(article);
                _context.SaveChanges();
            }

            var trandingArticles = _context.Articles
                .Include(x => x.Category)
                .OrderByDescending(x => x.ViewCount).Take(4).ToList();

            var tags = _context.Tags.ToList();

            DetailVM detailVM = new()
            {
                Article = article,
                TrandingArticles = trandingArticles,
                Tags = tags
            };

            return View(detailVM);
        }
     
    }
}
