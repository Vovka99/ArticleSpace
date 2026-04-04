var builder = DistributedApplication.CreateBuilder(args);

var postgresConnectionString = builder.AddConnectionString("DefaultConnection").Resource;
var database = builder.AddPostgres("postrgres-server")
	.WithConnectionStringRedirection(postgresConnectionString)
	.AddDatabase("ArticleSpaceDb");


var apiService = builder.AddProject<Projects.ArticleSpace_ApiService>("api")
	.WithReference(database)
	.WaitFor(database);

builder.AddProject<Projects.ArticleSpace_Web>("web")
	.WithExternalHttpEndpoints()
	.WithReference(apiService)
	.WaitFor(apiService);

builder.Build().Run();
