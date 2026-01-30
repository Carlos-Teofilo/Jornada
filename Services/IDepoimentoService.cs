using Jornada.Models;
using Jornada.Repositories;
using Jornada.ViewModels.Depoimentos;

namespace Jornada.Services;

public interface IDepoimentoService
{
    Task<DetailDepoimentoViewModel> CreateAsync(Usuario usuario, PostDepoimentoViewModel model);
    Task<List<ListDepoimentoViewModel>> GetAsync(string? nome, int page, int pageSize);
    Task<DetailDepoimentoViewModel> GetByIdAsync(int id);
    Task<bool> UpdateAsync(Usuario usuario, PutDepoimentoViewModel model, int id);
    Task<bool> DeleteAsync(Usuario usuario, int id);
}