using ClipperStreamingApp.Domain.Playlist.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ClipperStreamingApp.Api.Controllers;

[ApiController]
[Route("api/bandas")]
public class BandaController : ControllerBase
{
    private readonly IBandaRepository _bandaRepository;

    public BandaController(IBandaRepository bandaRepository)
    {
        _bandaRepository = bandaRepository;
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
    {
        var bandas = await _bandaRepository.SearchByNameAsync(q);
        
        return Ok(bandas);
    }
}