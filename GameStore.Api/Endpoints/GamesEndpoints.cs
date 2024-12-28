using System;
using GameSt.Api.Entities;
using GameStore.Api.Data;
using GameStore.Api.Dto;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints {
        
    const string GetGameEndpointName = "GetGame";
    private static readonly List<GameDto> games = [
        new (1, "street fighter", "fighting", 19.99M, new DateOnly(1992, 7, 15)),
    ];

    public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app) {
        
        var group = app.MapGroup("games").WithParameterValidation();;
        // GET /games
        group.MapGet("/", () => games);

        // GET /games
        group.MapGet("/{id}", (int id) => {
            GameDto? game = games.Find(game => game.Id == id);
            return game is null ? Results.NotFound() : Results.Ok(game);

        }).WithName(GetGameEndpointName);

        // POST /games
        group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) => {
            Game game = new() {
                Name = newGame.Name,
                Genre = dbContext.Genres.Find(newGame.GenreId),
                Price = newGame.Price,
                ReleaseDate = newGame.ReleaseDate
            };

            dbContext.Games.Add(game);
            dbContext.SaveChanges();

            GameDto gameDto = new(game.Id, game.Name, game.Genre!.Name, game.Price, game.ReleaseDate);
            
            return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gameDto);
        });

        // PUT /games
        group.MapPut("/{id}", (int id, UpdateGameDto updateGameDto) => {
            var index = games.FindIndex(game => game.Id == id);
            if (index == -1) {
                return Results.NotFound();
            }
            games[index] = new GameDto(id, updateGameDto.Name, updateGameDto.Genre, updateGameDto.Price, updateGameDto.ReleaseDate);
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) => {
            games.RemoveAll(game => game.Id == id);
            return Results.NoContent();
        });

        return group;
    }

}
