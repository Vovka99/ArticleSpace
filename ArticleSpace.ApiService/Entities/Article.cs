namespace ArticleSpace.ApiService.Entities
{
	public class Article
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public PublicationStatus Status { get; set; }
		public string Tag { get; set; }

	}

	public enum PublicationStatus
	{
		Draft,
		Published
	}
}
