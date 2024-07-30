using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebUI.Data;

namespace WebUI.ViewComponents
{
    public class FooterCategoryViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterCategoryViewComponent(AppDbContext context)
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
