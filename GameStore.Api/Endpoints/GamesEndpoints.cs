using GameStore.Api.Data;
using GameStore.Api.DTOs;
using GameStore.Api.Models;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndpointName = "GetGame"; // Local constant for GetGame endpoint

    // Shortest way to create lists
    private static readonly List<GameDto> games = 
    [
        new (1, "Street Fighter II", "Fighting", 19.99m, new DateOnly(1992, 7, 15)),
        new (2, "The Legend of Zelda: Ocarina of Time", "Action-Adventure", 29.99m, new DateOnly(1998, 11, 21)),
        new (3, "Minecraft", "Sandbox", 26.95m, new DateOnly(2011, 11, 18)),
        new (4, "The Witcher 3: Wild Hunt", "RPG", 39.99m, new DateOnly(2015, 5, 19)),
        new (5, "Portal 2", "Puzzle-Platformer", 19.99m, new DateOnly(2011, 4, 19)),
        new (6, "Super Mario Odyssey", "Platformer", 59.99m, new DateOnly(2017, 10, 27))
    ];

    public static void MapGamesEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/games").WithTags("Games"); // Grouping endpoints with the same prefix and adding tags for Swagger documentation

        // GET /games
        group.MapGet("/", () => games);

        // GET /games/{id}
        group.MapGet("/{id}", (int id ) =>
        {
            var game = games.Find(game => game.Id == id);
            return game is null 
                ? Results.NotFound() 
                : Results.Ok(game); // HTTP 404 if not found, otherwise HTTP 200 with the game in the response body
        })
            .WithName(GetGameEndpointName); // Introduce like the local constant to avoid typos and make it easier to change the endpoint name in the future

        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
        {
            Game game = new()
            {
                Name = newGame.Name,
                GenreId = newGame.GenreId, // Use GenreId instead of Genre name to create the game
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            GameDetailsDto gameDetailsDto = new(
                game.Id,
                game.Name,
                game.GenreId, // Use GenreId instead of Genre name to return the game details
                game.Price,
                game.ReleaseDate
            );

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = gameDetailsDto.Id}, gameDetailsDto); // HTTP 201 + Location header + response body
        });

        // PUT /games/{id}
        group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
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
        group.MapDelete("/{id}", (int id) =>
        {
            var index = games.FindIndex(game => game.Id == id);

            if (index == -1) // Check if the game with this index exists
                return Results.NotFound(); // HTTP 404

            games.RemoveAt(index);
            
            return Results.NoContent(); // HTTP 204
        });
    }
}
