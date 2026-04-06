
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient("ArticleSpaceApi", client =>
{
	client.BaseAddress = new Uri(builder.Configuration["ArticleSpaceApi:BaseUrl"] ?? "https://localhost:5001/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapStaticAssets();
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Articles}/{action=Index}/{id?}")
	.WithStaticAssets();

app.Run();
