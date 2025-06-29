using ClipperStreamingApp.Domain.Playlist;
using ClipperStreamingApp.Domain.Playlist.Repository;
using Microsoft.EntityFrameworkCore;

namespace ClipperStreamingApp.Infrastructure.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly StreamingDbContext _context;

    public PlaylistRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<Playlist?> GetByIdWithItemsAsync(int id)
    {
        return await _context.Playlists
            .Include(p => p.Musicas) 
            .Include(p => p.Bandas)  
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public async Task<Musica?> GetMusicaByIdAsync(int id)
    {
        return await _context.Musicas.FindAsync(id);
    }
    
    public async Task<Banda?> GetBandaByIdAsync(int id)
    {
        return await _context.Bandas.FindAsync(id);
    }

    public void Update(Playlist playlist)
    {
        _context.Playlists.Update(playlist);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}