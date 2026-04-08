using ArticleSpace.ApiService;
using ArticleSpace.ApiService.Data;
using ArticleSpace.ApiService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddApiVersioning(options =>
{
	options.AssumeDefaultVersionWhenUnspecified = true;
	options.DefaultApiVersion = new ApiVersion(1, 0);
	options.ReportApiVersions = true;
});

builder.Services.AddHttpClient("ExternalStoreApi", client =>
{
    client.BaseAddress = new Uri(builder.Configuration["DefaultConnection"] ?? "https://fakestoreapi.com/");
	client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddDbContext<AppDbContext>(options => 
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IArticleService, ArticleService>();
builder.Services.AddScoped<IExternalStoreService, ExternalStoreService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddOpenApi();

builder.Services.AddHostedService<ProductSyncHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
