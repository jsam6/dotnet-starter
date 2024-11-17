namespace GameStore.Api.Dto;

public record class GameDto(int Id, string name, string Genre, decimal Price, DateOnly ReleaseDate);