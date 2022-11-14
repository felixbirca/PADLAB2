using Common;
using Newtonsoft.Json;

namespace Movies.API.Helpers
{
    public class SyncNodeClient
    {
        private HttpClient _httpClient;

        public SyncNodeClient()
        {
            _httpClient = new HttpClient();
        }

        public async Task<SyncResponseDto?> GetNextNode(SyncRequestDto request)
        {
            var response = (await _httpClient.PostAsJsonAsync("http://sync-node:80/api/sync-data", request, default)).EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<SyncResponseDto>(await response.Content.ReadAsStringAsync());
        }

        public async Task RegisterNode(NodeInfoDto request)
        {
            (await _httpClient.PostAsJsonAsync("http://sync-node:80/api/register-node", request)).EnsureSuccessStatusCode();
        }
    }
}
