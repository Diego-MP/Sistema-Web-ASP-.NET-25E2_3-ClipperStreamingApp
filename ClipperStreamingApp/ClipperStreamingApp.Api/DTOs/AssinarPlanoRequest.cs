using System.ComponentModel.DataAnnotations;

namespace ClipperStreamingApp.Api.DTOs;
public class AssinarPlanoRequest
{
    [Required]
    public int ContaId { get; set; }
    [Required]
    public int PlanoId { get; set; }
}