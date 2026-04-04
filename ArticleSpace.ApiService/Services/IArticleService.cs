using ArticleSpace.ApiService.Entities;

namespace ArticleSpace.ApiService.Services
{
	public interface IArticleService
	{
		Task<string> CreateAsync(CreateArticleRequest request);
		Task DeleteAsync(string id);
		Task<List<Article>> GetAllAsync();
		Task<Article?> GetByIdAsync(string id);
		Task UpdateAsync(string id, UpdateArticleRequest request);
	}
}