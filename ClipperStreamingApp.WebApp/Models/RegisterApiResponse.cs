using System.Text.Json.Serialization;

namespace ClipperStreamingApp.WebApp.Models;

public class RegisterApiResponse
{
    [JsonPropertyName("$id")]
    public string Id { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get; set; }

    [JsonPropertyName("contaId")]
    public int ContaId { get; set; }
}