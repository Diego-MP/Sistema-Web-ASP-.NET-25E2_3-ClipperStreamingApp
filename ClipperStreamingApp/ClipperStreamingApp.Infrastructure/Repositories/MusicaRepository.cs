using ClipperStreamingApp.Domain.Playlist;
using ClipperStreamingApp.Domain.Musica.Repository;
using Microsoft.EntityFrameworkCore;

namespace ClipperStreamingApp.Infrastructure.Repositories;

public class MusicaRepository : IMusicaRepository
{
    private readonly StreamingDbContext _context;

    public MusicaRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Musica>> SearchByNameAsync(string term)
    {
        return await _context.Musicas
            .Include(m => m.Banda)
            .Where(m => m.Nome.ToLower().Contains(term.ToLower()))
            .ToListAsync();
    }
}