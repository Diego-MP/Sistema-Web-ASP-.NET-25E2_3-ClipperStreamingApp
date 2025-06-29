namespace ClipperStreamingApp.Domain.Assinatura;

public class Assinatura
{
        public int Id { get; set; }
        public Conta.Conta Conta { get; set; }
        public Plano Plano { get; set; }
        public bool Status { get; set; }
        public List<Transacao> Transacoes { get; set; }

}