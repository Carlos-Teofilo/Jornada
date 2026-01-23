using System.ComponentModel.DataAnnotations;

namespace Jornada.ViewModels.Destinos;

public class PutDestinoViewModel
{
    public string? Nome { get; set; }
    
    public int? Preco { get; set; }
    public string? Foto { get; set; }
}