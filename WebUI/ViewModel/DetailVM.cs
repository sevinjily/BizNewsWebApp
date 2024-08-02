using WebUI.Models;

namespace WebUI.ViewModel
{
    public class DetailVM
    {
        public Article Article{ get; set; }
        public List<Article> TrandingArticles { get; set; }
        public List<Tag> Tags { get; set; }
        public List<ArticleComment> ArticleComments { get; set; }


    }
}
