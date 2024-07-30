using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
//using WebUI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebUI.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
       private readonly AppDbContext _context;

        public SearchViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public  async Task<IViewComponentResult> InvokeAsync(string searchQuery)
        {
            var articles = await _context.Articles
                .Include(x => x.Category)
                .Include(x => x.ArticleTags)
                .Where(x => x.Title.Contains(searchQuery) || x.Content.Contains(searchQuery))
                .ToListAsync();
            return View("Search",articles);
        }
    }
}
