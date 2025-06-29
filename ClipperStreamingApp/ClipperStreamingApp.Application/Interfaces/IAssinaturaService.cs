namespace ClipperStreamingApp.Application.Interfaces;

public interface IAssinaturaService
{
    Task AssinarPlanoAsync(int contaId, int planoId);
}