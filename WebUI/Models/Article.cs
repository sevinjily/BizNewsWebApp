using System.ComponentModel.DataAnnotations;

namespace WebUI.Models
{
    public class Article
    {
        public Guid Id { get; set; }
        [MinLength(5)]
        public string Title { get; set; }
        public string Content { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public int ViewCount { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ArticleTag> ArticleTags { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string SeoUrl { get; set; }
        public List<ArticleComment> ArticleComments { get; set; }
        public List<CommentReply> CommentReplies { get; set; }


    }
}
