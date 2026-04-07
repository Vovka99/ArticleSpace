using ArticleSpace.ApiService.Models;

namespace ArticleSpace.ApiService.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> Get(string title, string category);
        Task<ProductDto> GetById(long id);
        Task SyncProducts();
    }
}