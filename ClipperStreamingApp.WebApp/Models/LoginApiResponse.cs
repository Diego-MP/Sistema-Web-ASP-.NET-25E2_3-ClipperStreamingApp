using System.Text.Json.Serialization;

namespace ClipperStreamingApp.WebApp.Models;

public class LoginApiResponse : ApiResponse
{
    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get; set; }
}