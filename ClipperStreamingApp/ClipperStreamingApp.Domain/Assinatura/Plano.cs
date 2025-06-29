namespace ClipperStreamingApp.Domain.Assinatura;

public class Plano
{
     public int Id { get; set; }
     public string Nome { get; set; }
     public decimal Valor { get; set; }
     
     public virtual ICollection<Assinatura> Assinaturas { get; set; } = new List<Assinatura>();
}