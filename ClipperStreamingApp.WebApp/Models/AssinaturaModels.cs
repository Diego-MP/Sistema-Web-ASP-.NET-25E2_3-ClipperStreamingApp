using System.ComponentModel.DataAnnotations;

namespace ClipperStreamingApp.WebApp.Models;

public class PlanoViewModel
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public decimal Valor { get; set; }
}

public class AssinaturaViewModel
{
    public List<PlanoViewModel> PlanosDisponiveis { get; set; } = new List<PlanoViewModel>();

    [Required(ErrorMessage = "Você precisa selecionar um plano.")]
    [Display(Name = "Plano Selecionado")]
    public int? SelectedPlanoId { get; set; }
}