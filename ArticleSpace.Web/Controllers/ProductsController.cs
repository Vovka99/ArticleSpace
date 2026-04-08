using ArticleSpace.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticleSpace.Web.Controllers
{
    public class ProductsController(IHttpClientFactory httpClientFactory, ILogger<ProductsController> logger) : Controller
    {
        public async Task<ActionResult> Index(string search, string category)
        {
            var client = httpClientFactory.CreateClient("ArticleSpaceApi");

            List<ProductViewModel> products = new();

            try
            {
                var url = "v1/products";
                var queryParts = new List<string>();
                if (!string.IsNullOrWhiteSpace(search))
                {
                    queryParts.Add($"title={search}");
                }

                if (!string.IsNullOrWhiteSpace(category))
                {
                    queryParts.Add($"category={category}");
                }

                if (queryParts.Count > 0)
                {
                    url += "?" + string.Join("&", queryParts);
                }

                var result = await client.GetFromJsonAsync<List<ProductViewModel>>(url);
                if (result is not null)
                {
                    products = result;
                }
            }
            catch (HttpRequestException exc)
            {
                logger.LogError(exc, "Error fetching products from API");
            }

            ViewData["Query"] = search ?? string.Empty;
            ViewData["Category"] = category ?? string.Empty;
            return View(products);
        }

        public async Task<ActionResult> Details(long id)
        {
            var client = httpClientFactory.CreateClient("ArticleSpaceApi");
            ProductViewModel? product = null;

            try
            {
                product = await client.GetFromJsonAsync<ProductViewModel>($"v1/products/{id}");
            }
            catch (HttpRequestException exc)
            {
                logger.LogError(exc, "Error fetching product {ProductId} from API", id);
            }

            if (product is null)
            {
                return NotFound("Product not found");
            }

            return View(product);
        }
    }
}
