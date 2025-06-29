using ClipperStreamingApp.Domain.Assinatura;
using System.Threading.Tasks;

namespace ClipperStreamingApp.Application.Interfaces;


public interface INotificacaoService
{

    Task Handle(TransacaoAutorizadaEvent @event);
}