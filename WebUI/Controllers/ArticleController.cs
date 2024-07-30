using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebUI.Data;
using WebUI.Models;
using WebUI.ViewModel;

namespace WebUI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public ArticleController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Detail(Guid id)
        {
            var article = _context.Articles
            .Include(x => x.Category)
            .Include(x => x.ArticleTags)
            .ThenInclude(x => x.Tag)
            .FirstOrDefault(x => x.Id == id);
           

            var cookie = _contextAccessor.HttpContext.Request.Cookies["Views"];
            string[] findCookie = { "" };

            if (cookie != null)
            {
                findCookie = cookie.Split('/').ToArray();
            }

            if (!findCookie.Contains(article.Id.ToString()))
            {
                Response.Cookies.Append($"Views", $"{cookie}/{article.Id}", new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    Expires = DateTime.Now.AddSeconds(10)
                });
                article.ViewCount += 1;
                _context.Articles.Update(article);
                _context.SaveChanges();
            }

            var trandingArticles = _context.Articles
                .Include(x => x.Category)
                .OrderByDescending(x => x.ViewCount).Take(4).ToList();


            var articleComment = _context.ArticleComments
                .Include(x=>x.User)
                .Include(x => x.Article)
                .Where(x=>x.ArticleId== article.Id)
                .ToList();

            var tags = _context.Tags.ToList();

            DetailVM detailVM = new()
            {
                Article = article,
                TrandingArticles = trandingArticles,
                Tags = tags,
                ArticleComments=articleComment
            };

            return View(detailVM);
        }
        public async Task<IActionResult> AddComment(string content,Guid articleId)
        {
          
            var userId= _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var username =  _contextAccessor.HttpContext.User.Identity.Name;
            
           

            ArticleComment articleComment = new()
            {
                Content = content,
                CreatedDate = DateTime.Now,
                UserId = userId,
                ArticleId = articleId,
                CreatedBy=username,
               

            };
         
            await _context.ArticleComments.AddAsync(articleComment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", "Article", new {Id=articleId});

        }
        
      
     
        public IActionResult DeleteComment(Guid commentId, Guid ArticleId)
        {
            var articleComment = _context.ArticleComments
        .Include(ac => ac.Article)
        .FirstOrDefault(x => x.Id == commentId);

            if (articleComment != null)
            {
                _context.ArticleComments.Remove(articleComment);
                _context.SaveChanges();
            }
                return RedirectToAction("Detail", "Article", new { Id = ArticleId });
           
        }
        public async  Task<IActionResult> ReplyComment(Guid commentId,string content)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var username = _contextAccessor.HttpContext.User.Identity.Name;
            ArticleCommentReply articleCommentReply = new()
            {
                CreatedDate = DateTime.Now,
                UserId = userId,
                CreatedBy = username,
                Content = content,
                ArticleCommentId = commentId
            };
            await _context.ArticleCommentReplies.AddAsync(articleCommentReply);
            await _context.SaveChangesAsync();
            return RedirectToAction("Detail", "Article", new { Id = commentId });
        }

    }
}
