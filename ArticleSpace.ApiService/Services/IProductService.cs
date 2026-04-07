using ArticleSpace.ApiService.Entities;

namespace ArticleSpace.ApiService.Services
{
    public interface IProductService
    {
        Task<List<Product>> Get(string title, string category);
        Task<Product?> GetById(long id);
        Task SyncProducts();
    }
}