using System.ComponentModel.DataAnnotations;

namespace Jornada.ViewModels.Destinos;

public class PostDestinoViewModel
{
    [Required(ErrorMessage = "O campo [Nome] é obrigatório!")]
    public string Nome { get; set; }
    
    [Required(ErrorMessage = "O campo [Preço] é obrigatório!")]
    public int Preco { get; set; }
    public string? Foto { get; set; }
}