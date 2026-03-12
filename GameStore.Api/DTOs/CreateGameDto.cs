namespace GameStore.Api.DTOs;

public record CreateGameDto (
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);

