using GameStore.Api.DTOs;

const string GetGameEndpointName = "GetGame"; // Local constant for GetGame endpoint

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

// GET /games/{id}
app.MapGet("/games/{id}", (int id ) => games.Find(game => game.Id == id))
    .WithName(GetGameEndpointName); // Introduce like the local constant to avoid typos and make it easier to change the endpoint name in the future

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

    return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game); // HTTP 201 + Location header + response body
});

// PUT /games/{id}
app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1) // Check if the game with this index exists
        return Results.NotFound(); // HTTP 404
    

    games[index] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.NoContent(); // HTTP 204
});

// DELETE /games/{id}
app.MapDelete("games/{id}", (int id) =>
{
    var index = games.FindIndex(game => game.Id == id);

    if (index == -1) // Need to check if the game is REALLY exists before trying to delete it
        return Results.NotFound(); // HTTP 404
    
    games.RemoveAt(index);

    return Results.NoContent(); // HTTP 204
});

app.Run();
