using ArticleSpace.ApiService.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticleSpace.ApiService.Data
{
	public class AppDbContext(DbContextOptions options) : DbContext(options)
	{
		public DbSet<Article> Articles => Set<Article>();
		public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Article>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Title).IsRequired().HasMaxLength(250);
				entity.Property(e => e.Content).IsRequired();
				entity.Property(e => e.CreatedAt).HasDefaultValueSql("NOW()");
				entity.Property(e => e.Status).IsRequired();
				entity.Property(e => e.Tag);

				entity.HasIndex(e => e.Title);
				entity.HasIndex(e => e.Tag);
			});

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(250);
				entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
				entity.Property(e => e.Description).IsRequired();
				entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
				entity.Property(e => e.ImageUrl).IsRequired();
				entity.Property(e => e.Rate).HasColumnType("double precision");
				entity.Property(e => e.RatingCount).HasColumnType("integer");

                entity.HasIndex(e => e.Title);
				entity.HasIndex(e => e.Category);
            });
        }
	}
}
