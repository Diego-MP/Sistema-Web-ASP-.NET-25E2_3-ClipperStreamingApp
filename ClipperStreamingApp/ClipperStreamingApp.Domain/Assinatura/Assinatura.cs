namespace ClipperStreamingApp.Domain.Assinatura;

public class Assinatura
{
        public int Id { get; set; }
        public Conta.Conta Conta { get; set; }
        public Plano Plano { get; set; }
        public bool Status { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();
        
        public Transacao CriarTransacao()
        {
                var novaTransacao = new Transacao
                {
                        Assinatura = this,
                        Valor = Plano.Valor,
                        Data = DateTime.UtcNow,
                        Status = true 
                };

                Transacoes.Add(novaTransacao);

                return novaTransacao;
        }

}