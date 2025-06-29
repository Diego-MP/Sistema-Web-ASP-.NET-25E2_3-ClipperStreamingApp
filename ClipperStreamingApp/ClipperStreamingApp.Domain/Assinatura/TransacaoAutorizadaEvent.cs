namespace ClipperStreamingApp.Domain.Assinatura;

public class TransacaoAutorizadaEvent
{
    public Transacao Transacao { get; }

    public TransacaoAutorizadaEvent(Transacao transacao)
    {
        Transacao = transacao ?? throw new ArgumentNullException(nameof(transacao));
    }
}