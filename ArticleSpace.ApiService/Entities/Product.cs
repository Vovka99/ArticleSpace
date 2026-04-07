namespace ArticleSpace.ApiService.Entities
{
    public class Product
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }

        public double Rate { get; set; }
        public int RatingCount { get; set; }
    }
}
