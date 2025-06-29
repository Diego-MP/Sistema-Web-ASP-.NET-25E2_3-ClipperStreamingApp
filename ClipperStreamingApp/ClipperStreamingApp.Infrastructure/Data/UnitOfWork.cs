using System.Threading.Tasks;
using ClipperStreamingApp.Application.Interfaces;

namespace ClipperStreamingApp.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly StreamingDbContext _context;

    public UnitOfWork(StreamingDbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}