using System.ComponentModel.DataAnnotations;

namespace Jornada.ViewModels.Depoimentos;

public class PutDepoimentoViewModel
{
    public string? Descricao { get; set; }
    public string? Foto { get; set; }
}