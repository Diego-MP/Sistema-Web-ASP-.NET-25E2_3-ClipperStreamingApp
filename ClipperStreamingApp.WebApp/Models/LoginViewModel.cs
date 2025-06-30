using System.ComponentModel.DataAnnotations;

namespace ClipperStreamingApp.WebApp.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "O campo Usuário é obrigatório.")]
    [Display(Name = "Usuário")]
    public string Username { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório.")]
    [DataType(DataType.Password)]
    [Display(Name = "Senha")]
    public string Password { get; set; }
}