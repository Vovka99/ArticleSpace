using ArticleSpace.ApiService.Entities;

namespace ArticleSpace.ApiService.Services
{
	public interface IArticleService
	{
		Task<string> Create(CreateArticleRequest request);
		Task Delete(string id);
		Task<List<Article>> Get(string title, string tag);
		Task<Article?> GetById(string id);
		Task Update(string id, UpdateArticleRequest request);
	}
}