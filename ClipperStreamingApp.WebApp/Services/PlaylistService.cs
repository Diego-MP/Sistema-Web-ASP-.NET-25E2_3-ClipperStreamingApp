using ClipperStreamingApp.WebApp.Models;
using System.Text.Json;

namespace ClipperStreamingApp.WebApp.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly HttpClient _httpClient;

        public PlaylistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<PlaylistViewModel>> GetUserPlaylistsAsync(int userId)
        {
            var endpoint = $"/api/Conta/{userId}/playlists";
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponseWrapper<PlaylistViewModel>>(endpoint);
                return response?.Values ?? new List<PlaylistViewModel>();
            }
            catch { return new List<PlaylistViewModel>(); }
        }

        public async Task<PlaylistDetalhesViewModel> GetPlaylistDetailsAsync(int playlistId)
        {
            var endpoint = $"/api/playlists/{playlistId}";
            try
            {
                return await _httpClient.GetFromJsonAsync<PlaylistDetalhesViewModel>(endpoint);
            }
            catch { return null; }
        }

        public async Task<bool> AddMusicaToPlaylistAsync(int playlistId, int musicaId)
        {
            var endpoint = $"/api/playlists/{playlistId}/musicas";
            var requestBody = new { musicaId };
            var response = await _httpClient.PostAsJsonAsync(endpoint, requestBody);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddBandaToPlaylistAsync(int playlistId, int bandaId)
        {
            var endpoint = $"/api/playlists/{playlistId}/bandas";
            var requestBody = new { bandaId };
            var response = await _httpClient.PostAsJsonAsync(endpoint, requestBody);
            return response.IsSuccessStatusCode;
        }
    }
}