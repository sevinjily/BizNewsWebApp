namespace WebUI.Models
{
    public class CommentReply
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
        public string? PhotoUrl { get; set; }
        public string Content { get; set; }
        public Guid ArticleCommentId { get; set; }
        public ArticleComment ArticleComment { get; set; }
    }
}
