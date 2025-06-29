using System.Threading.Tasks;

namespace ClipperStreamingApp.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}