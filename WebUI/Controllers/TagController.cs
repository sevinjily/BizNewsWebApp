using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;
using WebUI.Models;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(Guid tagId)
        {
         

           var tagArticles=_context.Articles
                .Include(x=>x.Category)
                .Include(x=>x.ArticleTags)
                .ThenInclude(x=>x.Tag)
                .Where(x => x.IsDeleted==false && x.ArticleTags.Any(at => at.TagId == tagId))
                .ToList();

         

            return View(tagArticles);
        }
    }
}
