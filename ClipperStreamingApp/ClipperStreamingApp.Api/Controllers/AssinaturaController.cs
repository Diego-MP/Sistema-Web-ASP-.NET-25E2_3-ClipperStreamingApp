using ClipperStreamingApp.Api.DTOs;
using ClipperStreamingApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClipperStreamingApp.Api.Controllers;

[ApiController]
[Route("api/assinaturas")]
public class AssinaturaController : ControllerBase
{
    private readonly IAssinaturaService _assinaturaService;

    public AssinaturaController(IAssinaturaService assinaturaService)
    {
        _assinaturaService = assinaturaService;
    }

    [HttpPost]
    public async Task<IActionResult> AssinarPlano([FromBody] AssinarPlanoRequest request)
    {
        try
        {
            await _assinaturaService.AssinarPlanoAsync(request.ContaId, request.PlanoId);
            
            return Ok(new { message = "Assinatura realizada e transação processada com sucesso!" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
