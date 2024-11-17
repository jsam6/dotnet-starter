using GameStore.Api.Dto;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

const string GetGameEndpointName = "GetGame";
List<GameDto> games = [
    new (1, "street fighter", "fighting", 19.99M, new DateOnly(1992, 7, 15)),
];

// GET /games
app.MapGet("games", () => games);

// GET /games
app.MapGet("games/{id}", (int id) => games.Find(game => game.Id == id)).WithName(GetGameEndpointName);


app.MapPost("games", (CreateGameDto newGame) => {
    GameDto game = new( games.Count + 1, newGame.Name, newGame.Genre, newGame.Price, newGame.ReleaseDate);
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});


app.Run();
