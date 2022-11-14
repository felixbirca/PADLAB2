using Movies.API.DTOs;

namespace Movies.API.Helpers
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task CreateMovie(CreateMovieDto request, string nodeUrl) 
        {
            (await _httpClient.PostAsJsonAsync(new Uri($"http://{nodeUrl}:80/api/movies/create"), request)).EnsureSuccessStatusCode();
        }

        public async Task UpdateMovie(EditMovieDto request, string nodeUrl)
        {
            (await _httpClient.PostAsJsonAsync(new Uri($"http://{nodeUrl}:80/api/movies/edit"), request)).EnsureSuccessStatusCode();
        }

        public async Task DeleteMovie(DeleteMovieDto request, string nodeUrl)
        {
            (await _httpClient.PostAsJsonAsync(new Uri($"http://{nodeUrl}:80/api/movies/delete"), request)).EnsureSuccessStatusCode();
        }
    }
}
