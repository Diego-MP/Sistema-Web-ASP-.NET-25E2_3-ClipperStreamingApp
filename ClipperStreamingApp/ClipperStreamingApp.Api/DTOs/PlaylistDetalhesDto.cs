namespace ClipperStreamingApp.Api.DTOs;

public class PlaylistDetalhesDto
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public List<MusicaDto> Musicas { get; set; } = new List<MusicaDto>();
}