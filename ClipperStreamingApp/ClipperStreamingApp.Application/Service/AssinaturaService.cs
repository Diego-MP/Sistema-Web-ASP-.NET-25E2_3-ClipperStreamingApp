using ClipperStreamingApp.Application.Interfaces;
using ClipperStreamingApp.Domain.Assinatura;
using ClipperStreamingApp.Domain.Assinatura.Repository;
using ClipperStreamingApp.Domain.Conta.Repository;
using ClipperStreamingApp.Domain.Plano.Repository;

namespace ClipperStreamingApp.Application.Services;

public class AssinaturaService : IAssinaturaService
{
    private readonly IContaRepository _contaRepository;
    private readonly IPlanoRepository _planoRepository;
    private readonly IAssinaturaRepository _assinaturaRepository;
    private readonly IUnitOfWork _unitOfWork; 
    private readonly INotificacaoService _notificacaoService; 

    public AssinaturaService(IContaRepository contaRepository, IPlanoRepository planoRepository, IAssinaturaRepository assinaturaRepository, IUnitOfWork unitOfWork, INotificacaoService notificacaoService)
    {
        _contaRepository = contaRepository;
        _planoRepository = planoRepository;
        _assinaturaRepository = assinaturaRepository;
        _unitOfWork = unitOfWork;
        _notificacaoService = notificacaoService;
    }

    public async Task AssinarPlanoAsync(int contaId, int planoId)
    {
        var conta = await _contaRepository.GetByIdAsync(contaId);
        if (conta == null) throw new KeyNotFoundException("Conta não encontrada.");

        var plano = await _planoRepository.GetByIdAsync(planoId);
        if (plano == null) throw new KeyNotFoundException("Plano não encontrado.");

        var novaAssinatura = new Assinatura
        {
            Conta = conta,
            Plano = plano,
            Status = true
        };
        _assinaturaRepository.Add(novaAssinatura);
        
        var novaTransacao = novaAssinatura.CriarTransacao();

        await _unitOfWork.SaveChangesAsync();
        
        var evento = new TransacaoAutorizadaEvent(novaTransacao);
        await _notificacaoService.Handle(evento);
    }
    
    public async Task<IEnumerable<Assinatura>> GetAssinaturasByContaIdAsync(int contaId)
    {
        var conta = await _contaRepository.GetByIdWithAssinaturasAsync(contaId);

        if (conta == null)
        {
            throw new KeyNotFoundException("Conta não encontrada.");
        }
        
        return conta.Assinaturas;
    }
}