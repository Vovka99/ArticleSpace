using ArticleSpace.ApiService.Data;
using ArticleSpace.ApiService.Entities;
using ArticleSpace.ApiService.Models;
using Microsoft.EntityFrameworkCore;

namespace ArticleSpace.ApiService.Services
{
    public class ProductService(AppDbContext context, IExternalStoreService externalStoreService) : IProductService
    {
        public async Task<List<ProductDto>> Get(string title, string category)
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

            var products = await query.ToListAsync();
            
            return products.Select(MapToDto).ToList();
        }

        public async Task<ProductDto> GetById(long id)
        {
            var product = await context.Products.FindAsync(id);
            return MapToDto(product);
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

        private ProductDto MapToDto(Product product)
        {
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Rating = new Rating
                {
                    Rate = product.Rate,
                    Count = product.RatingCount
                }
            };
        }
    }
}
