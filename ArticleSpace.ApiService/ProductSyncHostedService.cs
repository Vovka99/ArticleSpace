using ArticleSpace.ApiService.Data;
using ArticleSpace.ApiService.Services;

namespace ArticleSpace.ApiService
{
    public class ProductSyncHostedService(IServiceProvider serviceProvider) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var productService = scope.ServiceProvider.GetRequiredService<IProductService>();

            if (!context.Products.Any())
            {
                await productService.SyncProducts();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
