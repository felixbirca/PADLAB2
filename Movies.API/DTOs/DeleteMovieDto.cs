namespace Movies.API.DTOs
{
    public class DeleteMovieDto : BaseRequest
    {
        public Guid Id { get; set; }
    }
}
