using ClipperStreamingApp.Application.Interfaces;
using ClipperStreamingApp.Domain.Playlist;
using ClipperStreamingApp.Domain.Conta.Repository;
using ClipperStreamingApp.Domain.Playlist.Repository;

namespace ClipperStreamingApp.Application.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IBandaRepository _bandaRepository;
    private readonly IContaRepository _contaRepository;

    public PlaylistService(IPlaylistRepository playlistRepository, IBandaRepository bandaRepository, IContaRepository contaRepository)
    {
        _playlistRepository = playlistRepository;
        _bandaRepository = bandaRepository;
        _contaRepository = contaRepository;
    }
    
    public async Task<IEnumerable<Playlist>> GetPlaylistsByContaIdAsync(int contaId)
    {
        var conta = await _contaRepository.GetByIdWithPlaylistsAsync(contaId);

        if (conta == null)
            throw new KeyNotFoundException("Conta não encontrada.");

        return conta.Playlists;
    }

    public async Task<Playlist?> GetPlaylistDetailsByIdAsync(int playlistId)
    {
        var playlist = await _playlistRepository.GetByIdWithItemsAsync(playlistId);

        return playlist;
    }
    
    public async Task AdicionarMusicaAPlaylistAsync(int playlistId, int musicaId)
    {
        var playlist = await _playlistRepository.GetByIdWithItemsAsync(playlistId);
        if (playlist == null)
            throw new KeyNotFoundException("Playlist não encontrada.");

        var musica = await _playlistRepository.GetMusicaByIdAsync(musicaId);
        if (musica == null)
            throw new KeyNotFoundException("Música não encontrada.");
            
        playlist.AdicionarMusica(musica);
        
        _playlistRepository.Update(playlist);
        await _playlistRepository.SaveChangesAsync();
    }
    
    public async Task AdicionarMusicasDaBandaAsync(int playlistId, int bandaId)
    {
        var playlist = await _playlistRepository.GetByIdWithItemsAsync(playlistId);
        if (playlist == null)
            throw new KeyNotFoundException("Playlist não encontrada.");

        var banda = await _bandaRepository.GetByIdWithMusicasAsync(bandaId);
        if (banda == null)
            throw new KeyNotFoundException("Banda não encontrada.");
            
        playlist.AdicionarMusicas(banda.Musicas);
        
        _playlistRepository.Update(playlist);
        await _playlistRepository.SaveChangesAsync();
    }
}