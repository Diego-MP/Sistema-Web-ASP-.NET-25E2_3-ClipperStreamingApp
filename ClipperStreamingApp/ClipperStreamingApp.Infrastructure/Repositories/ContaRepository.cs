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

    public async Task<Conta?> GetByIdAsync(int id)
    {
        return await _context.Contas.FindAsync(id);
    }

    public async Task<Conta?> GetByIdWithPlaylistsAsync(int id)
    {
        return await _context.Contas
            .Include(c => c.Playlists)
            .ThenInclude(p => p.Musicas) 
            .FirstOrDefaultAsync(c => c.Id == id);
    }
    
    public async Task<Conta?> GetByIdWithAssinaturasAsync(int id)
    {
        return await _context.Contas
            .Include(c => c.Assinaturas)         
            .ThenInclude(a => a.Plano)       
            .Include(c => c.Assinaturas)
            .ThenInclude(a => a.Transacoes)  
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
