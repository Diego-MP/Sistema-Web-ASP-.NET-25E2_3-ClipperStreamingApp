using System.ComponentModel.DataAnnotations;

namespace ClipperStreamingApp.Api.DTOs;

public class RegistrarUsuarioRequest
{
    [Required]
    public string Nome { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    [MinLength(6)]
    public string Password { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}