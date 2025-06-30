using ClipperStreamingApp.WebApp.Models;

namespace ClipperStreamingApp.WebApp.Services;

public class SearchService : ISearchService
{
    private readonly HttpClient _httpClient;

    public SearchService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<BandaSearchResultViewModel>> SearchBandasAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<BandaSearchResultViewModel>();
        var endpoint = $"/api/bandas/search?q={query}";
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponseWrapper<BandaSearchResultViewModel>>(endpoint);
            return response?.Values ?? new List<BandaSearchResultViewModel>();
        }
        catch { return new List<BandaSearchResultViewModel>(); }
    }

    public async Task<List<MusicaSearchResultViewModel>> SearchMusicasAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query)) return new List<MusicaSearchResultViewModel>();
        var endpoint = $"/api/musicas/search?q={query}";
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ApiResponseWrapper<MusicaSearchResultViewModel>>(endpoint);
            return response?.Values ?? new List<MusicaSearchResultViewModel>();
        }
        catch { return new List<MusicaSearchResultViewModel>(); }
    }
}