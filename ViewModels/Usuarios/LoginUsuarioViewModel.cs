using System.ComponentModel.DataAnnotations;

namespace Jornada.ViewModels;

public class LoginUsuarioViewModel
{
    [Required(ErrorMessage = "Este campo é obrigatório")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Este campo é obrigatório")]
    public string Senha { get; set; }
}