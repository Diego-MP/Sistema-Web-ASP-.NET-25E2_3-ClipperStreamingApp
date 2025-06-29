namespace ClipperStreamingApp.Domain.Conta.Repository;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByUsernameAsync(string username);

}