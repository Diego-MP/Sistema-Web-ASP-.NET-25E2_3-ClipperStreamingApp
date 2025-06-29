using ClipperStreamingApp.Domain.Conta;

namespace ClipperStreamingApp.Application.Interfaces;

public interface IAuthService
{
    Task<Usuario?> AutenticarAsync(string username, string password);
}