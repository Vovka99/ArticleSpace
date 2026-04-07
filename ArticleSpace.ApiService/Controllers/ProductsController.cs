using ArticleSpace.ApiService.Entities;
using ArticleSpace.ApiService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArticleSpace.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> Get(string? title, string? tag)
        {
            var articles = await productService.Get(title, tag);
            return Ok(articles);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<Product>> Get(long id)
        {
            var article = await productService.GetById(id);
            if (article == null) return NotFound();

            return Ok(article);
        }
    }
}
