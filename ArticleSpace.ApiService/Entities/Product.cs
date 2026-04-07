namespace ArticleSpace.ApiService.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public decimal Price { get; set; }
        public string Description { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        public double Rate { get; set; }
        public int RatingCount { get; set; }
    }
}
