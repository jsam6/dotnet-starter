using System;
using GameStore.Api.Dto;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints {
        
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new (1, "street fighter", "fighting", 19.99M, new DateOnly(1992, 7, 15)),
    ];

    public static WebApplication MapGamesEndpoints(this WebApplication app) {
        
        // GET /games
        app.MapGet("games", () => games);

        // GET /games
        app.MapGet("games/{id}", (int id) => {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        // POST /games
        app.MapPost("games", (CreateGameDto newGame) => {
            GameDto game = new( games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
            games.Add(game);

            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
        });

        // PUT /games
        app.MapPut("games/{id}", (int id, UpdateGameDto updateGameDto) => {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1) {
                return Results.NotFound();
            }
            games[index] = new GameDto(id, updateGameDto.Name, updateGameDto.Genre, updateGameDto.Price, updateGameDto.ReleaseDate);
            return Results.NoContent();
        });

        app.MapDelete("games/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return app;
    }

}
