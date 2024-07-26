using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        public List<Article> Index(string searchQuery)
        {
            var articles=_context.Articles
                .Where(x=>x.Title.Contains(searchQuery)||x.Content.Contains(searchQuery))
                .ToList();  
            return articles;
        }
    }
}
