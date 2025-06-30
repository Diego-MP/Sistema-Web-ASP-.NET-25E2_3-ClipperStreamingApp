namespace ClipperStreamingApp.WebApp.Models;

public class HistoricoAssinaturaViewModel
{
    public int Id { get; set; }
    public string NomePlano { get; set; }
    public decimal ValorPlano { get; set; }
    public bool Status { get; set; }
    public DateTime DataUltimaTransacao { get; set; }
}