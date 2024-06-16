using Azure;
using Microsoft.AspNetCore.Mvc;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                ViewBag.Error = "The Category name is required!";
                return View(category);
            }
            var findCategory=_context.Categories.FirstOrDefault(x=>x.CategoryName==category.CategoryName);
            if (findCategory != null)
            {
                ViewBag.Error = "This category name is already exists!";
                return View(findCategory);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Redirect("/admin/category/index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var findCategories=_context.Categories.FirstOrDefault(x=>x.Id==id);
            if (findCategories == null)
            {
                return NotFound();
            }
            return View(findCategories);
        }
        [HttpPost]
        public IActionResult Edit(Category category) 
        {

            var findCategory = _context.Categories.FirstOrDefault(x => x.CategoryName.ToLower() == category.CategoryName.ToLower());
            if (findCategory != null)
            {
                ViewBag.Error = "This category name is already exists!";
                return View(findCategory);
            }
            _context.Categories.Update(category);
                _context.SaveChanges();
            return Redirect("/admin/category/index");
        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var findCategories = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (findCategories == null)
            {
                return NotFound();
            }
            return View(findCategories);
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Redirect("/admin/category/index");
        }
    }
}
