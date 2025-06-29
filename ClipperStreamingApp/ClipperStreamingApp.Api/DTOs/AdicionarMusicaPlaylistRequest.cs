using System.ComponentModel.DataAnnotations;

namespace ClipperStreamingApp.Api.DTOs;

public class AdicionarMusicaPlaylistRequest
{
    [Required]
    public int MusicaId { get; set; }
}