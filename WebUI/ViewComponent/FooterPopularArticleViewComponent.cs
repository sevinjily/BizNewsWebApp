using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
namespace WebUI.ViewComponents
{
    public class FooterPopularArticleViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterPopularArticleViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var trandingArticles= await _context.Articles
                .Include(x=>x.Category).ToListAsync();
            return View("FooterPopularArticle",trandingArticles);

        }
    }
}
