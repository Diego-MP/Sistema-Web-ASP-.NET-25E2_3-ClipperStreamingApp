using ClipperStreamingApp.Domain;
using ClipperStreamingApp.Domain.Conta.Repository;
using ClipperStreamingApp.Domain.Conta;
using Microsoft.EntityFrameworkCore;

namespace ClipperStreamingApp.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly StreamingDbContext _context;
    
    public UsuarioRepository(StreamingDbContext context)
    {
        _context = context;
    }
    
    public async Task<Usuario?> GetByUsernameAsync(string username)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
    }
}