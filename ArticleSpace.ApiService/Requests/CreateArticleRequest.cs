using System.ComponentModel.DataAnnotations;

public class CreateArticleRequest
{
	[Required]
	[StringLength(250, MinimumLength = 3)]
	public string Title { get; set; }

	[Required]
	[StringLength(5000, MinimumLength = 10)]
    public string Content { get; set; }

	[StringLength(100)]
	public string Tag { get; set; }
}
