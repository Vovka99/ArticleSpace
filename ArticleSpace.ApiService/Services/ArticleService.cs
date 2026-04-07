using ArticleSpace.ApiService.Data;
using ArticleSpace.ApiService.Entities;
using ArticleSpace.ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleSpace.ApiService.Services
{
	public class ArticleService(AppDbContext context) : IArticleService
	{
		public async Task<List<ArticleDto>> Get(string title, string tag)
		{
			var query = context.Articles.AsQueryable();

			if (!string.IsNullOrWhiteSpace(title))
			{
				var pattern = $"%{title}%";
				query = query.Where(a => EF.Functions.ILike(a.Title, pattern));
			}

			if (!string.IsNullOrWhiteSpace(tag))
			{
				query = query.Where(a => a.Tag == tag);
			}

			var articles = await query.ToListAsync();
            return articles.Select(MapToDto).ToList();
        }

		public async Task<ArticleDto> GetById(string id)
		{
            var article = await context.Articles.FindAsync(id);
            return MapToDto(article);
        }

		public async Task<string> Create(CreateArticleRequest request)
		{
			var article = new Article
			{
				Id = Guid.NewGuid().ToString(),
				Title = request.Title,
				Content = request.Content,
				CreatedAt = DateTime.UtcNow,
				Status = Entities.PublicationStatus.Draft,
				Tag = request.Tag
			};

			context.Articles.Add(article);
			await context.SaveChangesAsync();

			return article.Id;
		}

		public async Task Update(string id, UpdateArticleRequest request)
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

		public async Task Delete(string id)
		{
			var article = await context.Articles.FindAsync(id);
			if (article == null)
			{
				throw new Exception("Article not found");
			}

			context.Articles.Remove(article);
			await context.SaveChangesAsync();
		}

		private ArticleDto MapToDto(Article article)
		{
			return new ArticleDto
			{
				Id = article.Id,
				Title = article.Title,
				Content = article.Content,
				CreatedAt = article.CreatedAt,
				Status = (Models.PublicationStatus)article.Status,
				Tag = article.Tag
			};
        }
    }
}
