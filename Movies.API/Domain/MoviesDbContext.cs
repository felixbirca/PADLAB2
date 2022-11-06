using Microsoft.EntityFrameworkCore;
using Movies.API.Domain.Entities;

namespace Movies.API.Domain
{
    public class MoviesDbContext : DbContext
    {
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MovieLike> MovieLikes { get; set; }
    }
}
