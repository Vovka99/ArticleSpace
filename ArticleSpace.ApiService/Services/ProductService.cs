using ArticleSpace.ApiService.Data;
using ArticleSpace.ApiService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticleSpace.ApiService.Services
{
    public class ProductService(AppDbContext context, IExternalStoreService externalStoreService) : IProductService
    {
        public async Task<List<Product>> Get(string title, string category)
        {
            var query = context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                var pattern = $"%{title}%";
                query = query.Where(a => EF.Functions.ILike(a.Title, pattern));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(a => a.Category == category);
            }

            return await query.ToListAsync();
        }

        public async Task<Product?> GetById(long id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task SyncProducts()
        {
            var externalProducts = await externalStoreService.GetProducts();

            foreach (var externalProduct in externalProducts)
            {
                var existingProduct = await context.Products.FindAsync((long)externalProduct.Id);
                if (existingProduct != null)
                {
                    existingProduct.Title = externalProduct.Title;
                    existingProduct.Category = externalProduct.Category;
                    existingProduct.Price = externalProduct.Price;
                    existingProduct.Description = externalProduct.Description;
                    existingProduct.ImageUrl = externalProduct.Image;
                    existingProduct.Rate = externalProduct.Rating.Rate;
                    existingProduct.RatingCount = externalProduct.Rating.Count;
                }
                else
                {
                    var newProduct = new Product
                    {
                        Id = externalProduct.Id,
                        Title = externalProduct.Title,
                        Category = externalProduct.Category,
                        Price = externalProduct.Price,
                        Description = externalProduct.Description,
                        ImageUrl = externalProduct.Image,
                        Rate = externalProduct.Rating.Rate,
                        RatingCount = externalProduct.Rating.Count
                    };

                    context.Products.Add(newProduct);
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
