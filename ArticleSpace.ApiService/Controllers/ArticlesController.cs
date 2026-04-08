using ArticleSpace.ApiService.Models;
using ArticleSpace.ApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticleSpace.ApiService.Controllers
{
	[ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
	public class ArticlesController(IArticleService articleService) : ControllerBase
	{
        [HttpGet]
        public async Task<ActionResult<List<ArticleDto>>> Get(string? title, string? tag)
		{
			var articles = await articleService.Get(title, tag);
			return Ok(articles);
		}

        [HttpGet("{id}")]
		public async Task<ActionResult<ArticleDto>> Get(string id)
		{
			var article = await articleService.GetById(id);
			if (article == null) return NotFound();

			return Ok(article);
		}

		[HttpPost]
		public async Task<ActionResult<string>> Create(CreateArticleRequest createRequest)
		{
			var id = await articleService.Create(createRequest);
			return CreatedAtAction(nameof(Get), new { id }, id);
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Update(string id, UpdateArticleRequest updateRequest)
		{
			await articleService.Update(id, updateRequest);
			
			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(string id)
		{
			await articleService.Delete(id);
			return NoContent();
		}
	}
}
