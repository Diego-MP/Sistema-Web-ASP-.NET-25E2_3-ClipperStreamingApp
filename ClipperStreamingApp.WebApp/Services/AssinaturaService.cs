using System.Text.Json;
using ClipperStreamingApp.WebApp.Models;

namespace ClipperStreamingApp.WebApp.Services
{
    public class AssinaturaService : IAssinaturaService
    {
        private readonly HttpClient _httpClient;

        public AssinaturaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<PlanoViewModel>> GetPlanosAsync()
        {
            try
            {
                var endpoint = "/api/planos";

                var apiResponse = await _httpClient.GetFromJsonAsync<ApiResponse.PlanosApiResponse>(endpoint);

                return apiResponse?.Values ?? new List<PlanoViewModel>();
            }
            catch (JsonException jsonEx)
            {
                Console.WriteLine($"Erro de desserialização JSON ao buscar planos: {jsonEx.Message}");
                return new List<PlanoViewModel>();
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($"Erro de HTTP ao buscar planos: {httpEx.Message}");
                return new List<PlanoViewModel>();
            }
        }
        public async Task<(bool IsSuccess, string Message)> AssinarPlanoAsync(int contaId, int planoId)
        {
            var endpoint = "/api/assinaturas";
            var requestBody = new { contaId, planoId };
            
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(endpoint, requestBody);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseContent);

            return (response.IsSuccessStatusCode, apiResponse?.Message ?? "Não foi possível processar a assinatura.");
        }

        public async Task<List<HistoricoAssinaturaViewModel>> GetHistoricoAssinaturasAsync(int contaId)
        {
            var endpoint = $"/api/Conta/{contaId}/assinaturas";
            try
            {
                var response = await _httpClient.GetFromJsonAsync<ApiResponseWrapper<HistoricoAssinaturaViewModel>>(endpoint);
                return response?.Values ?? new List<HistoricoAssinaturaViewModel>();
            }
            catch
            {
                return new List<HistoricoAssinaturaViewModel>();
            }        
        }
    }
}
