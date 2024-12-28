
using GameSt.Api.Entities;
using GameStore.Api.Dto;

namespace GameStore.Api.Mapping;


public static class GenreMapping {
    public static GenreDto ToDto(this Genre genre) {
        return new GenreDto(genre.Id, genre.Name);
    }
}