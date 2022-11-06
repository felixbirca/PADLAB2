namespace Movies.API.Domain.Entities
{
    public class MovieLike
    {
        public Movie Movie { get; set; }
        public Guid MovieId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
