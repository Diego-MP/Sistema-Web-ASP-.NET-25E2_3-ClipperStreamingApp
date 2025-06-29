using ClipperStreamingApp.Domain.Assinatura;

namespace ClipperStreamingApp.Domain.Conta.Factory;

public interface IContaFactory
{
    Conta CriarConta(Usuario usuario, Plano planoPadrao = null);
}