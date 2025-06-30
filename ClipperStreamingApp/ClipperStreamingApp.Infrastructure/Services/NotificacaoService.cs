using ClipperStreamingApp.Domain.Assinatura;

using ClipperStreamingApp.Application.Interfaces;

namespace ClipperStreamingApp.Infrastructure.Services;

public class NotificacaoService : INotificacaoService
{
    public Task Handle(TransacaoAutorizadaEvent @event)
    {
        Console.WriteLine("----- INICIANDO ENVIO DE NOTIFICAÇÃO -----");
        Console.WriteLine($"Simulando envio de notificação para a conta: {@event.Transacao.Assinatura.Conta.Id}");
        Console.WriteLine($"Referente à transação autorizada: {@event.Transacao.Id}");
        Console.WriteLine($"Valor: {@event.Transacao.Valor:C}");
        Console.WriteLine("----- NOTIFICAÇÃO ENVIADA COM SUCESSO -----");
        
        return Task.CompletedTask;
    }
}