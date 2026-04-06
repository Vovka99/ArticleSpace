namespace ArticleSpace.Web.Models
{
    public class ArticleViewModel
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public PublicationStatus Status { get; set; }
        public string Tag { get; set; } = string.Empty;
    }

    public enum PublicationStatus
    {
        Draft,
        Published
    }   
}
