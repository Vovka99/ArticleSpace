using ArticleSpace.ApiService.Data;
using ArticleSpace.ApiService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticleSpace.ApiService.Services
{
	public class ArticleService(AppDbContext context) : IArticleService
	{
		public async Task<List<Article>> GetAllAsync()
		{
			return await context.Articles.ToListAsync();
		}

		public async Task<Article?> GetByIdAsync(string id)
		{
			return await context.Articles.FindAsync(id);
		}

		public async Task<string> CreateAsync(CreateArticleRequest request)
		{
			var article = new Article
			{
				Id = Guid.NewGuid().ToString(),
				Title = request.Title,
				Content = request.Content,
				CreatedAt = DateTime.UtcNow,
				Status = PublicationStatus.Draft,
				Tag = request.Tag
			};

			context.Articles.Add(article);
			await context.SaveChangesAsync();

			return article.Id;
		}

		public async Task UpdateAsync(string id, UpdateArticleRequest request)
		{
			var article = await context.Articles.FindAsync(id);
			if (article == null)
			{
				throw new Exception("Article not found");
			}

			article.Title = request.Title;
			article.Content = request.Content;
			article.Status = request.Status;
			article.Tag = request.Tag;

			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(string id)
		{
			var article = await context.Articles.FindAsync(id);
			if (article == null)
			{
				throw new Exception("Article not found");
			}

			context.Articles.Remove(article);
			await context.SaveChangesAsync();
		}
	}
}
