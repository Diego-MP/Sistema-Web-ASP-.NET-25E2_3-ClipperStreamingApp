using System.ComponentModel.DataAnnotations;

namespace ClipperStreamingApp.Api.DTOs;

public class AdicionarBandaPlaylistRequest
{
    [Required]
    public required int BandaId { get; set; }
}