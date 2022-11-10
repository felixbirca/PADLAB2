using Movies.API.Helpers;
using System.Text.Json.Serialization;

namespace Movies.API.DTOs
{
    public class ViewMovieDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public long Budget { get; set; }
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly ReleaseDate { get; set; }
        public IEnumerable<string> Actors { get; set; }
    }
}
