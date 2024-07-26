using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Models;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Guid id)
        {
            var categoryArticles = _context.Articles
              .Include(x => x.Category)
            .Include(x => x.ArticleTags)
            .ThenInclude(x => x.Tag)
              .Where(x => x.IsDeleted == false && x.CategoryId==id)
              .ToList();

           
            return View (categoryArticles);
        }
    }
}
