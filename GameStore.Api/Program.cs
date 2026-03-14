using GameStore.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGamesEndpoints(); // Map the endpoints defined in GamesEndpoints class
app.Run();
