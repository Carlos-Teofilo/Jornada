using System.ComponentModel.DataAnnotations;

namespace Jornada.ViewModels.UsuarioViewModel;

public class RegisterUsuarioViewModel
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Nome é obrigatório")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Senha é obrigatório")]
    public string Senha { get; set; }
}