using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); // Add validation services to the dependency injection container

var app = builder.Build();

app.MapGamesEndpoints(); // Map the endpoints defined in GamesEndpoints class

app.Run();
