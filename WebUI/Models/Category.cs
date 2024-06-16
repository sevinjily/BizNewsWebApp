namespace WebUI.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public List<Article> Articles { get; set; }
    }
}
