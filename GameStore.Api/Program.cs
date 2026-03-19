using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); // Add validation services to the dependency injection container

builder.AddGameStoreDb(); // Seed the database with initial data if it doesn't already exist

var app = builder.Build();

app.MapGamesEndpoints(); // Map the endpoints defined in GamesEndpoints class

app.MigrateDb(); // Apply any pending migrations to the database

app.Run();
