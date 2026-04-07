using ArticleSpace.ApiService.Models;

namespace ArticleSpace.ApiService.Services
{
    public class ExternalStoreService(IHttpClientFactory httpClientFactory, ILogger<ExternalStoreService> logger) : IExternalStoreService
    {
        public async Task<List<ExternalStoreProductDto>> GetProducts()
        {
            try
            {
                var httpClient = httpClientFactory.CreateClient("ExternalStoreApi");
                var products = await httpClient.GetFromJsonAsync<List<ExternalStoreProductDto>>("products");

                return products ?? new List<ExternalStoreProductDto>();
            }
            catch (HttpRequestException exc)
            {
                logger.LogError(exc, "Error fetching products from external store API");
                return new List<ExternalStoreProductDto>();
            }
        }
    }
}
