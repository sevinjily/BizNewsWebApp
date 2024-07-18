using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebUI.Data;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;

        public TagController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tags = _context.Tags.ToList();
            return View(tags);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            if (string.IsNullOrEmpty(tag.TagName))
            {
                ViewBag.Error = "The Tag name is required!";
                return View(tag);
            }
            var findTag = _context.Tags.FirstOrDefault(x => x.TagName.ToLower() == tag.TagName.ToLower());
            if (findTag != null)
            {
                ViewBag.Error = "This tag name is already exists!";
                return View(findTag);
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return Redirect("/admin/tag/index");
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var findTag = _context.Tags.FirstOrDefault(y => y.Id == id);
            if (findTag == null)
            {
                return NotFound();
            }
            return View(findTag);
        }
        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            var findTag = _context.Tags.FirstOrDefault(x => x.Id== tag.Id);
            bool checkTagName = true;
            byte[] AsciibytesUI=Encoding.ASCII.GetBytes(tag.TagName);
            byte[] AsciibytesDb=Encoding.ASCII.GetBytes(findTag.TagName);
            if (tag.TagName == findTag.TagName)
            {

            for (int i = 0; i < tag.TagName.Length; i++)
            {
                    if (AsciibytesUI[i]!= AsciibytesDb[i])
                    {
                    checkTagName = false;
                        break;
                    }
                }
            }
            else
            {
                checkTagName = false;
            }
            
            if (checkTagName)
            {
                ViewBag.Error = "This tag name is already exists!";
                return View(findTag);   
            }
            findTag.TagName = tag.TagName;
            _context.Tags.Update(findTag);
            _context.SaveChanges();
            return Redirect("/admin/tag/index");

        }
        [HttpGet]
        public IActionResult Delete(Guid id)
        {
            var deleteTag = _context.Tags.FirstOrDefault(y => y.Id == id);
            if (deleteTag == null)
            {
                return NotFound();
            }
            return View(deleteTag);
        }
        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
           
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return Redirect("/admin/tag/index");

        }



    }
}

