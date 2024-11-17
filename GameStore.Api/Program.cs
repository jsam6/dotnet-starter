using GameStore.Api.Dto;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

List<GameDto> games = [
    new (1, "street fighter", "fighting", 19.99M, new DateOnly(1992, 7, 15)),
];

// GET /games
app.MapGet("games", () => games);

// GET /games
app.MapGet("/games/{id}", (int id) => games.Find(game => game.Id == id));


app.Run();
