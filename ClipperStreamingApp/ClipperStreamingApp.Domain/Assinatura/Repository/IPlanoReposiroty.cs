namespace ClipperStreamingApp.Domain.Plano.Repository;

public interface IPlanoRepository
{
    Task<Assinatura.Plano?> GetByIdAsync(int id);
    Task<IEnumerable<Assinatura.Plano>> GetAllAsync();
}