using ClipperStreamingApp.WebApp.Models;

namespace ClipperStreamingApp.WebApp.Services;

public interface ISearchService
{
    Task<List<BandaSearchResultViewModel>> SearchBandasAsync(string query);
    Task<List<MusicaSearchResultViewModel>> SearchMusicasAsync(string query);
}