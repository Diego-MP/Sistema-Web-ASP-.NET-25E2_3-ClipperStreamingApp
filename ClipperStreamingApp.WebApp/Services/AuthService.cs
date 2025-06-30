using System.Text.Json;
using ClipperStreamingApp.WebApp.Models;

namespace ClipperStreamingApp.WebApp.Services;

 public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<(bool IsSuccess, string Message, int? UserId)> LoginAsync(string username, string password)
        {
            var loginRequest = new { username, password };
            var endpoint = "/api/Auth/login";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, loginRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = JsonSerializer.Deserialize<LoginApiResponse>(responseContent);
                return (true, apiResponse?.Message, apiResponse?.UsuarioId);
            }

            var errorResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);
            return (false, errorResponse?.Message ?? "Erro desconhecido.", null);
        }

        public async Task<(bool IsSuccess, string Message)> RegisterAsync(RegisterViewModel model)
        {
            var registerRequest = new { nome = model.Nome, username = model.Username, password = model.Password, email = model.Email };
            var endpoint = "/api/Conta/Registrar";
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, registerRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);
            return (response.IsSuccessStatusCode, apiResponse?.Message ?? "Erro ao processar o registro.");
        }
    }
