using Jornada.Models;
using Jornada.ViewModels.Depoimentos;

namespace Jornada.Repositories;

public interface IDepoimentoRepository
{
    Task<(List<Depoimento>, int Total)> GetAllAsync(int page, int pageSize);
    Task<Depoimento?> GetByIdAsync(int id);
    Task<Depoimento> CreateAsync(Depoimento depoimento);
    Task<bool> UpdateAsync(Usuario usuario, Depoimento depoimento, int id);
    Task<bool> DeleteAsync(Usuario usuario, int id);
}