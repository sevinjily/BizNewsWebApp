using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;

namespace WebUI.ViewComponents
{
    public class FooterCategory:ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterCategory(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()

        {
            var categories=_context.Categories.Include(x=>x.Articles).ToList();

            return View("FooterCategory", categories);
        }

    }
}
