var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postrgres");
var postgresDb = postgres.AddDatabase("articleSpaceDb");


var apiService = builder.AddProject<Projects.ArticleSpace_ApiService>("api")
	.WaitFor(postgresDb)
	.WithReference(postgresDb);

builder.AddProject<Projects.ArticleSpace_Web>("web")
	.WithExternalHttpEndpoints()
	.WithReference(apiService)
	.WaitFor(apiService);

builder.Build().Run();
