using ArticleSpace.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ArticleSpace.Web.Controllers
{
    public class ArticlesController(IHttpClientFactory httpClientFactory, ILogger<ArticlesController> logger) 
        : Controller
    {
        public async Task<ActionResult> Index()
        {
            var client = httpClientFactory.CreateClient("ArticleSpaceApi");

            List<ArticleViewModel> articles = new();

            try
            {
                var result = await client.GetFromJsonAsync<List<ArticleViewModel>>("articles");
                if (result is not null)
                {
                    articles = result;
                }
            }
            catch (HttpRequestException exc)
            {
                logger.LogError(exc, "Error fetching articles from API");
            }

            return View(articles);
        }

        public async Task<ActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Article ID is required");
            }

            var client = httpClientFactory.CreateClient("ArticleSpaceApi");
            ArticleViewModel? article = null;

            try
            {
                article = await client.GetFromJsonAsync<ArticleViewModel>($"articles/{id}");
            }
            catch (HttpRequestException exc)
            {
                logger.LogError(exc, $"Error fetching article {id} from API");
            }

            if (article is null)
            {
                return NotFound($"Article not found");
            }

            return View(article);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
