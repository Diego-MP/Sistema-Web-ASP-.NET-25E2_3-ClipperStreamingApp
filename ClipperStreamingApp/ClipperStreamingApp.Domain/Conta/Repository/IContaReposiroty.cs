namespace ClipperStreamingApp.Domain.Conta.Repository;

public interface IContaRepository
{
    Task<Conta?> GetByIdWithPlaylistsAsync(int id);
    Task<Conta> GetByIdAsync(int contaId);
    Task<Conta?> GetByIdWithAssinaturasAsync(int id);
}