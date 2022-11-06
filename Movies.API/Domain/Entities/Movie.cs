namespace Movies.API.Domain.Entities
{
    public class Movie
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public long Budget { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public string Actors { get; set; }
    }
}
