using GameStore.Api.Data;
using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation(); // Add validation services to the dependency injection container

var connectionString = "Data Source=Data/GameStore.db";
builder.Services.AddSqlite<GameStoreContext>(connectionString); // Register the GameStoreContext with the SQLite database connection string

var app = builder.Build();

app.MapGamesEndpoints(); // Map the endpoints defined in GamesEndpoints class

app.Run();
