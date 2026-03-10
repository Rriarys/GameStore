namespace GameStore.Api.DTOs;

// record → DTO (API request/response)
// class  → Entity model (EF Core / database)
public record GameDto (
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate
);
