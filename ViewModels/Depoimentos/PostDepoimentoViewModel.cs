using System.ComponentModel.DataAnnotations;

namespace Jornada.ViewModels.Depoimentos;

public class PostDepoimentoViewModel
{
    [Required(ErrorMessage = "Este campo é obrigatório!")]
    public string Descricao { get; set; }
    public string? Foto { get; set; }

}