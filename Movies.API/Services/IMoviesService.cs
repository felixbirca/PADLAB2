using Movies.API.Domain.Entities;
using Movies.API.DTOs;
using Movies.API.Endpoints;

namespace Movies.API.Services
{
    public interface IMoviesService
    {
        Task Create(CreateMovieDto request);
        Task DeleteMovie(Guid id);
        Task EditMovie(EditMovieDto request);
        Task<IEnumerable<ViewMovieDto>> GetAllMovies();
        Task<Movie?> GetMovieById(Guid id);
        Task LikeMovie(Guid movieId, int userId);
    }
}