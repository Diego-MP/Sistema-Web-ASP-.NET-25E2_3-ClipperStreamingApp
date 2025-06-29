using ClipperStreamingApp.Api.DTOs;
using ClipperStreamingApp.Application.Interfaces;
using ClipperStreamingApp.Domain.Conta.Factory;
using ClipperStreamingApp.Domain.Conta;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClipperStreamingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContaController : ControllerBase
{
    private readonly StreamingDbContext _context;
    private readonly IContaFactory _contaFactory;
    private readonly IPlaylistService _playlistService;

    public ContaController(
        StreamingDbContext context,
        IContaFactory contaFactory,
        IPlaylistService playlistService) 
    {
        _context = context;
        _contaFactory = contaFactory;
        _playlistService = playlistService;
    }

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar(RegistrarUsuarioRequest request)
    {
        var novoUsuario = new Usuario
        {
            Nome = request.Nome,
            Username = request.Username,
            Password = request.Password, 
            Email = request.Email
        };
        
        var planoGratuito = await _context.Planos
                                          .FirstOrDefaultAsync(p => p.Nome == "Gratuito");

        var novaConta = _contaFactory.CriarConta(novoUsuario, planoGratuito);
        
        _context.Contas.Add(novaConta);

        await _context.SaveChangesAsync();

        return Ok(new
        {
            message = "Usuário e conta criados com sucesso!",
            usuarioId = novoUsuario.Id,
            contaId = novaConta.Id
        });
    }
    
    [HttpGet]
    public async Task<IActionResult> ListarTodas()
    {
        var contas = await _context.Contas
            .Include(c => c.Usuario)
            .ToListAsync();

        return Ok(contas);
    }
    
    [HttpGet("hello")] 
    public IActionResult GetHelloWorld()
    {
        return Ok("Hello World!");
    }
    
    [HttpGet("{contaId}/playlists")]
    public async Task<IActionResult> GetPlaylistsDaConta(int contaId)
    {
        try
        {
            var playlists = await _playlistService.GetPlaylistsByContaIdAsync(contaId);

            var playlistsDto = playlists.Select(p => new PlaylistResumoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                QuantidadeMusicas = p.Musicas.Count
            });

            return Ok(playlistsDto);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}