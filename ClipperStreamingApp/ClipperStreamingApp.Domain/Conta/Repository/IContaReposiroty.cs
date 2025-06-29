namespace ClipperStreamingApp.Domain.Conta.Repository;

public interface IContaRepository
{
    Task<Conta?> GetByIdWithPlaylistsAsync(int id);
}