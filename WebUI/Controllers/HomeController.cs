using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebUI.Data;
using WebUI.Models;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var featuredArticles=_context.Articles
                .Where(x=>x.IsFeatured==true&&x.IsDeleted==false)
                .Include(x=>x.Category) 
                .OrderByDescending(x=>x.UpdatedDate)
                .Take(4).ToList();
            HomeVM homeVM = new() 
            { 
                FeaturedArticles=featuredArticles 
            };

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
