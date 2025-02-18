namespace GameStore.Api.Dto;

public record class GameSummaryDto(int Id, string name, string Genre, decimal Price, DateOnly ReleaseDate);