using ClipperStreamingApp.Domain.Conta;
using ClipperStreamingApp.Domain.Conta.Repository;
using Microsoft.EntityFrameworkCore;

namespace ClipperStreamingApp.Infrastructure.Repositories;

public class ContaRepository : IContaRepository
{
    private readonly StreamingDbContext _context;

    public ContaRepository(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<Conta?> GetByIdWithPlaylistsAsync(int id)
    {
        return await _context.Contas
            .Include(c => c.Playlists) 
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}