using ClipperStreamingApp.Application.Interfaces;
using ClipperStreamingApp.Domain.Conta;
using ClipperStreamingApp.Domain.Conta.Repository;

namespace ClipperStreamingApp.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public AuthService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Usuario?> AutenticarAsync(string username, string password)
    {
        var usuario = await _usuarioRepository.GetByUsernameAsync(username);

        if (usuario == null)
        {
            return null;
        }

        if (!usuario.VerificarPassword(password)) 
        {
            return null;
        }
        
        return usuario;
    }
}