namespace ArticleSpace.ApiService.Models
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public Rating Rating { get; set; }
    }
}
