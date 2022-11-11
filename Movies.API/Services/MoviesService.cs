using Microsoft.EntityFrameworkCore;
using Movies.API.Domain;
using Movies.API.Domain.Entities;
using Movies.API.DTOs;

namespace Movies.API.Services
{
    public class MoviesService : IMoviesService
    {
        private MoviesDbContext _context;

        public MoviesService(MoviesDbContext context)
        {
            _context = context;
        }

        public async Task<Movie?> GetMovieById(Guid id)
        {
            return await _context.Movies.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(CreateMovieDto request)
        {
            await _context.Movies.AddAsync(new Movie
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Actors = string.Join(", ", request.Actors),
                Budget = request.Budget,
                ReleaseDate = request.RleaseDate
            });

            await _context.SaveChangesAsync();
        }

        public async Task EditMovie(EditMovieDto request)
        {
            var movie = await _context.Movies.Where(x => x.Id == request.Id).FirstAsync();

            movie.Title = request.Title == null ? movie.Title : request.Title;
            movie.Budget = request.Budget == null ? movie.Budget : (long)request.Budget;
            movie.Actors = request.Actors == null ? movie.Actors : string.Join(", ", request.Actors);
            movie.ReleaseDate = request.RleaseDate == null ? movie.ReleaseDate : (DateOnly)request.RleaseDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMovie(Guid id)
        {
            var movie = await _context.Movies.Where(x => x.Id == id).FirstAsync();
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
        }

        public async Task LikeMovie(Guid movieId, int userId)
        {
            await _context.MovieLikes.AddAsync(new MovieLike { MovieId = movieId, UserId = userId });
        }

        public async Task<IEnumerable<ViewMovieDto>> GetAllMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies.Select(x => new ViewMovieDto
            {
                Id = x.Id,
                Title = x.Title,
                Budget = x.Budget,
                Actors = x.Actors.Split(", "),
                ReleaseDate = x.ReleaseDate
            });
        }
    }
}
