using ClipperStreamingApp.Api.DTOs;
using ClipperStreamingApp.Application.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClipperStreamingApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var usuario = await _authService.AutenticarAsync(request.Username, request.Password);

        if (usuario == null)
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim("ContaId", usuario.Conta.Id.ToString()) 
        };
        
        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            new ClaimsPrincipal(claimsIdentity));
        
        var response = new LoginResponseDto
        {
            Message = "Login realizado com sucesso.",
            UsuarioId = usuario.Id,
            ContaId = usuario.Conta.Id 
        };

        return Ok(response);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Ok(new { message = "Logout realizado com sucesso." });
    }
}
