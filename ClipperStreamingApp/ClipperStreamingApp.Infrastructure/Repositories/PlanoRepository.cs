using ClipperStreamingApp.Domain.Assinatura;
using ClipperStreamingApp.Domain.Plano.Repository;

namespace ClipperStreamingApp.Infrastructure.Repositories;

public class PlanoRepository : IPlanoRepository
{
    private readonly StreamingDbContext _context;
    public PlanoRepository(StreamingDbContext context) { _context = context; }
    public async Task<Plano?> GetByIdAsync(int id) => await _context.Planos.FindAsync(id);
}
