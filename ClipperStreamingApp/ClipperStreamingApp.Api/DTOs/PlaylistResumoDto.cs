namespace ClipperStreamingApp.Api.DTOs;
public class PlaylistResumoDto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public int QuantidadeMusicas { get; set; }
}