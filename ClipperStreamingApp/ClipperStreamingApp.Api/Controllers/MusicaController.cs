using ClipperStreamingApp.Api.DTOs;
using ClipperStreamingApp.Domain.Musica.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ClipperStreamingApp.Api.Controllers;

[ApiController]
[Route("api/musicas")]
public class MusicaController : ControllerBase
{
    private readonly IMusicaRepository _musicaRepository;

    public MusicaController(IMusicaRepository musicaRepository)
    {
        _musicaRepository = musicaRepository;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        var musicas = await _musicaRepository.SearchByNameAsync(q);

        var musicasDto = musicas.Select(m => new MusicaDto
        {
            Id = m.Id,
            Nome = m.Nome,
            NomeBanda = m.Banda?.Nome ?? "Desconhecida"
        });

        return Ok(musicasDto);
    }
}