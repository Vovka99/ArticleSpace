using ArticleSpace.ApiService.Models;
using ArticleSpace.ApiService.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArticleSpace.ApiService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> Get(string? title, string? tag)
        {
            var articles = await productService.Get(title, tag);
            return Ok(articles);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ProductDto>> Get(long id)
        {
            var article = await productService.GetById(id);
            if (article == null) return NotFound();

            return Ok(article);
        }
    }
}
