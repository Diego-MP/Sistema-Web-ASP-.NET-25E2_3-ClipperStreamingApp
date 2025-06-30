using ClipperStreamingApp.WebApp.Models;

namespace ClipperStreamingApp.WebApp.Services;

public interface IAssinaturaService
{
    Task<List<PlanoViewModel>> GetPlanosAsync();
    Task<(bool IsSuccess, string Message)> AssinarPlanoAsync(int usuarioId, int planoId);
    Task<List<HistoricoAssinaturaViewModel>> GetHistoricoAssinaturasAsync(int contaId);

}