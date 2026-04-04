using ArticleSpace.ApiService.Entities;

public class UpdateArticleRequest
{
	public string Title { get; set; }
	public string Content { get; set; }
	public PublicationStatus Status { get; set; }
	public string Tag { get; set; }
}
