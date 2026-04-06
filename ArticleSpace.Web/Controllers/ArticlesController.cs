using ArticleSpace.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticleSpace.Web.Controllers
{
    public class ArticlesController(IHttpClientFactory httpClientFactory, ILogger<ArticlesController> logger) 
        : Controller
    {
        [Route("/")]
        [Route("articles")]
        [Route("articles/tag/{tag}")]
        [Route("articles/search/{search}")]
        public async Task<ActionResult> Index(string search, string tag)
        {
            var client = httpClientFactory.CreateClient("ArticleSpaceApi");

            List<ArticleViewModel> articles = new();

            try
            {
                var url = "articles";
                var queryParts = new List<string>();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    queryParts.Add($"title={search}");
                }

                if (!string.IsNullOrWhiteSpace(tag))
                {
                    queryParts.Add($"tag={tag}");
                }

                if (queryParts.Count > 0)
                {
                    url += "?" + string.Join("&", queryParts);
                }

                var result = await client.GetFromJsonAsync<List<ArticleViewModel>>(url);
                if (result is not null)
                {
                    articles = result;
                }
            }
            catch (HttpRequestException exc)
            {
                logger.LogError(exc, "Error fetching articles from API");
            }

            ViewData["Query"] = search ?? string.Empty;
            ViewData["Tag"] = tag ?? string.Empty;
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
    }
}
