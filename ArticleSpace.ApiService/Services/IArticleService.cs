using ArticleSpace.ApiService.Models;

namespace ArticleSpace.ApiService.Services
{
	public interface IArticleService
	{
		Task<string> Create(CreateArticleRequest request);
		Task Delete(string id);
		Task<List<ArticleDto>> Get(string title, string tag);
		Task<ArticleDto> GetById(string id);
		Task Update(string id, UpdateArticleRequest request);
	}
}