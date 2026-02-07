using Jornada.Models;

namespace Jornada.ViewModels.Depoimentos;

public class DetailDepoimentoViewModel
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public IList<string>? Fotos { get; set; }
    public string Usuario { get; set; }
}