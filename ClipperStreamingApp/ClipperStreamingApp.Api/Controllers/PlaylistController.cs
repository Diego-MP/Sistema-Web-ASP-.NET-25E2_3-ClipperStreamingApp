using ClipperStreamingApp.Api.DTOs;
using ClipperStreamingApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClipperStreamingApp.Api.Controllers;

[ApiController]
[Route("api/playlists")]
public class PlaylistController : ControllerBase
{
    private readonly IPlaylistService _playlistService;

    public PlaylistController(IPlaylistService playlistService)
    {
        _playlistService = playlistService;
    }

    [HttpPost("{playlistId}/musicas")]
    public async Task<IActionResult> AdicionarMusica(int playlistId, [FromBody] AdicionarMusicaPlaylistRequest request)
    {
        try
        {
            await _playlistService.AdicionarMusicaAPlaylistAsync(playlistId, request.MusicaId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception  ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("{playlistId}/bandas")]
    public async Task<IActionResult> AdicionarMusicasDaBanda(int playlistId, [FromBody] AdicionarBandaPlaylistRequest request)
    {
        try
        {
            await _playlistService.AdicionarMusicasDaBandaAsync(playlistId, request.BandaId);
            return NoContent(); 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception  ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpGet("{playlistId}")]
    public async Task<IActionResult> GetPlaylistPorId(int playlistId)
    {
        var playlist = await _playlistService.GetPlaylistDetailsByIdAsync(playlistId);

        if (playlist == null)
        {
            return NotFound(new { message = "Playlist não encontrada." });
        }

        var playlistDto = new PlaylistDetalhesDto
        {
            Id = playlist.Id,
            Nome = playlist.Nome,
            Musicas = playlist.Musicas.Select(m => new MusicaDto
            {
                Id = m.Id,
                Nome = m.Nome,
                NomeBanda = m.Banda?.Nome ?? "Desconhecida"
            }).ToList()
        };

        return Ok(playlistDto);
    }
}