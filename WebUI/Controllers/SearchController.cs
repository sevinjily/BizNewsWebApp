using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                .Include(x=>x.ArticleComments)
                .Where(x => x.Title.Contains(searchQuery) || x.Content.Contains(searchQuery))
                .ToList();
                   

            return View(articles);
           
        }
    }
}
