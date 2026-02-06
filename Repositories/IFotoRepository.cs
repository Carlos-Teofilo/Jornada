using Jornada.Models;

namespace Jornada.Repositories;

public interface IFotoRepository
{
    Task<Foto> CreateAsync(Foto foto);
    Task<Foto?> GetByUrlAsync(string url);
}