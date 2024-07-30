namespace WebUI.Models
{
    public class ArticleCommentReply
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }   
        public string UserId { get; set; }
        public User User { get; set; }
        public string Content { get; set; }
        public Guid ArticleCommentId { get; set; }
        public ArticleComment ArticleComment { get; set; }
    }
}
