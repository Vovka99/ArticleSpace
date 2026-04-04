using ArticleSpace.ApiService.Entities;
using ArticleSpace.ApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticleSpace.ApiService.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ArticlesController(IArticleService articleService) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<List<Article>>> Get(string title, string tag)
		{
			var articles = await articleService.GetAllAsync();
			return Ok(articles);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Article>> Get(string id)
		{
			var article = await articleService.GetByIdAsync(id);
			if (article == null) return NotFound();

			return Ok(article);
		}

		[HttpPost]
		public async Task<ActionResult<string>> Create(CreateArticleRequest createRequest)
		{
			var id = await articleService.CreateAsync(createRequest);
			return CreatedAtAction(nameof(Get), new { id }, id);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(string id, UpdateArticleRequest updateRequest)
		{
			await articleService.UpdateAsync(id, updateRequest);
			
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id)
		{
			await articleService.DeleteAsync(id);
			return NoContent();
		}
	}
}
