using ArticleSpace.ApiService.Entities;
using System.ComponentModel.DataAnnotations;

public class UpdateArticleRequest
{
    [Required]
    [StringLength(250, MinimumLength = 3)]
    public string Title { get; set; }

    [Required]
    [StringLength(5000, MinimumLength = 10)]
    public string Content { get; set; }

    [EnumDataType(typeof(PublicationStatus))]
    public PublicationStatus Status { get; set; }

    [StringLength(100)]
    public string Tag { get; set; }
}
