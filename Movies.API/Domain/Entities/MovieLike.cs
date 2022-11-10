using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movies.API.Domain.Entities
{
    [Keyless]
    public class MovieLike
    {
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
