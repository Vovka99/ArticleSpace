namespace ArticleSpace.Web.Models
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public RatingViewModel Rating { get; set; }
    }

    public class RatingViewModel
    {
        public double Rate { get; set; }
        public int Count { get; set; }
    }
}
