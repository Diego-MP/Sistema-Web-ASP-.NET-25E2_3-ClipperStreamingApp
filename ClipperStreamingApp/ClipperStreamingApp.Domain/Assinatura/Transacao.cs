namespace ClipperStreamingApp.Domain;

public class Transacao
{
    public int Id { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public bool Status { get; set; }
    
    public int AssinaturaId { get; set; } 
    public virtual Assinatura Assinatura { get; set; } 

}