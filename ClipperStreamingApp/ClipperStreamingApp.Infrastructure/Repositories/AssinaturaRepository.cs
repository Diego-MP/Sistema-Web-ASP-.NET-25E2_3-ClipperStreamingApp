using ClipperStreamingApp.Domain.Assinatura;
using ClipperStreamingApp.Domain.Assinatura.Repository;

namespace ClipperStreamingApp.Infrastructure.Repositories;

public class AssinaturaRepository : IAssinaturaRepository
{
    private readonly StreamingDbContext _context;
    public AssinaturaRepository(StreamingDbContext context) { _context = context; }
    public void Add(Assinatura assinatura) => _context.Assinaturas.Add(assinatura);
}