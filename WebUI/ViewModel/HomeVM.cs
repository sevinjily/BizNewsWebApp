using WebUI.Models;

namespace WebUI.ViewModel
{
    public class HomeVM
    {

        public List<Article> FeaturedArticles { get; set; }
        public List<Article> TrandingArticles { get; set; }

        public List<Article> LatestArticles { get; set; }
        public List<Tag> Tags { get; set; }
        public Dictionary<Guid, int> ArticleCommentCounts { get; set; }

    }
}
