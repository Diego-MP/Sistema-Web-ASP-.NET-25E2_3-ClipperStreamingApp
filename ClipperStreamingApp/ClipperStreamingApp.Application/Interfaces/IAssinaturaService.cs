using ClipperStreamingApp.Domain.Assinatura;

namespace ClipperStreamingApp.Application.Interfaces;

public interface IAssinaturaService
{
    Task AssinarPlanoAsync(int contaId, int planoId);
    Task<IEnumerable<Assinatura>> GetAssinaturasByContaIdAsync(int contaId);

}