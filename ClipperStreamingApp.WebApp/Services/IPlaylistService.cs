using ClipperStreamingApp.WebApp.Models;

namespace ClipperStreamingApp.WebApp.Services;

public interface IPlaylistService
{
    Task<List<PlaylistViewModel>> GetUserPlaylistsAsync(int userId);
    Task<PlaylistDetalhesViewModel> GetPlaylistDetailsAsync(int playlistId);
    Task<bool> AddMusicaToPlaylistAsync(int modelSelectedPlaylistId, int modelSelectedItemId);
    Task<bool> AddBandaToPlaylistAsync(int modelSelectedPlaylistId, int modelSelectedItemId);
}
