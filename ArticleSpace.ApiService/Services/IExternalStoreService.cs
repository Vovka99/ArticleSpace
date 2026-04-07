using ArticleSpace.ApiService.Models;

namespace ArticleSpace.ApiService.Services
{
    public interface IExternalStoreService
    {
        Task<List<ExternalStoreProductDto>> GetProducts();
    }
}