using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebUI.Data;
using WebUI.Models;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchQuery)
        {
            var articles = _context.Articles
                .Include(x => x.Category)
                .Include(x => x.ArticleTags)
                .Where(x => x.Title.Contains(searchQuery) || x.Content.Contains(searchQuery))
                .ToList();
            var articleVMs = new List<ArticleVM>();

            foreach (var article in articles)
            {
                var commentCount = _context.ArticleComments.Count(x => x.ArticleId == article.Id);
                articleVMs.Add(new ArticleVM { Article = article, CommentCount = commentCount });
            }

            return View(articleVMs);
           
        }
    }
}
