using Jornada.Models;

namespace Jornada.ViewModels.Depoimentos;

public class ListDepoimentoViewModel
{
    public IList<DetailDepoimentoViewModel> Depoimentos { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    
}