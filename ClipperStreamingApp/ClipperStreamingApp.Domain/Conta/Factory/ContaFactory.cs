using ClipperStreamingApp.Domain.Assinatura;
using ClipperStreamingApp.Domain.Playlist;

namespace ClipperStreamingApp.Domain.Conta.Factory;

public class ContaFactory : IContaFactory
{
    public Conta CriarConta(Usuario usuario, Plano planoPadrao = null)
    {
        if (usuario == null)
        {
            throw new ArgumentNullException(nameof(usuario), "Usuário é obrigatório para criar uma conta.");
        }

        var novaConta = new Conta
        {
            Usuario = usuario
        };

        var playlistInicial = new Playlist.Playlist
        {
            Conta = novaConta,
            Nome = "Favoritas"
        };
        novaConta.Playlists.Add(playlistInicial);
        
        if (planoPadrao != null)
        {
            var assinaturaInicial = new Assinatura.Assinatura
            {
                Conta = novaConta,
                Plano = planoPadrao, 
                Status = true
            };
            novaConta.Assinaturas.Add(assinaturaInicial);
        }
        return novaConta;
    }
}