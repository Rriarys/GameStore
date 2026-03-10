using GameStore.Api.DTOs;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

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

app.Run();
