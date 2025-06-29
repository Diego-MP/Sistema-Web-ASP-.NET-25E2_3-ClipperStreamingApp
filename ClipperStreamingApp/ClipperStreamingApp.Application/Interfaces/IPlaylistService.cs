using ClipperStreamingApp.Domain.Playlist;

namespace ClipperStreamingApp.Application.Interfaces;

public interface IPlaylistService
{
    Task AdicionarMusicaAPlaylistAsync(int playlistId, int musicaId);
    Task AdicionarMusicasDaBandaAsync(int playlistId, int bandaId);

    Task<IEnumerable<Playlist>> GetPlaylistsByContaIdAsync(int contaId);
    
    Task<Playlist?> GetPlaylistDetailsByIdAsync(int playlistId);
}