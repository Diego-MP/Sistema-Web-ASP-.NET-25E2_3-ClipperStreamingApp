using ClipperStreamingApp.Domain.Assinatura;

namespace ClipperStreamingApp.Domain.Conta.Factory;

public interface IContaFactory
{
    Conta CriarConta(Usuario usuario, Assinatura.Plano planoPadrao = null);
}