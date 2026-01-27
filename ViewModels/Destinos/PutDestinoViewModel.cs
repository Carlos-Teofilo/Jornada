using System.ComponentModel.DataAnnotations;

namespace Jornada.ViewModels.Destinos;

public class PutDestinoViewModel
{
    public string? Nome { get; set; }
    
    public int? Preco { get; set; }
    public string? Foto { get; set; }
    public string? Foto2 { get; set; }
    public string? Meta { get; set; }
    public string? TextoDescritivo { get; set; }
}