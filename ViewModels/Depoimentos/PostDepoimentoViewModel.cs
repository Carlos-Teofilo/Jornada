using System.ComponentModel.DataAnnotations;
using Jornada.Models;

namespace Jornada.ViewModels.Depoimentos;

public class PostDepoimentoViewModel
{
    [Required(ErrorMessage = "Este campo é obrigatório!")]
    public string Descricao { get; set; }
    public List<string> Fotos { get; set; }
}