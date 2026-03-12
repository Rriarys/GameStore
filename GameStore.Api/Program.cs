using GameStore.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Shortest way to create lists
List<GameDto> games = [
    new (1, "Street Fighter II", "Fighting", 19.99m, new DateOnly(1992, 7, 15)),
    new (2, "The Legend of Zelda: Ocarina of Time", "Action-Adventure", 29.99m, new DateOnly(1998, 11, 21)),
    new (3, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
    new (4, "The Witcher 3: Wild Hunt", "RPG", 39.99m, new DateOnly(2015, 5, 19)),
    new (5, "Portal 2", "Puzzle-Platformer", 19.99m, new DateOnly(2011, 4, 19)),
    new (6, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27))
];

// GET /games
app.MapGet("/games", () => games);

const string GetGameEndpointName = "GetGame";

// GET /games/{id}
app.MapGet("/games/{id}", (int id ) => games.Find(game => game.Id == id))
    .WithName(GetGameEndpointName); // Introduce like the local constant to avoid typos and make it easier to change the endpoint name in the future.

// POST /games
app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new (
        games.Count + 1, // New Id
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);

    return Results.CreatedAtRoute("GetGame", new {id = game.Id}, game); // HTTP 201 + Location header + response body. 
});

app.Run();
