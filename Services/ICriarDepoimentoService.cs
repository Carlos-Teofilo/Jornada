using Jornada.Models;
using Jornada.ViewModels.Depoimentos;

namespace Jornada.Services;

public interface ICriarDepoimentoService
{
    Task<DetailDepoimentoViewModel> ExecuteAsync(Usuario usuario, PostDepoimentoViewModel model);
}