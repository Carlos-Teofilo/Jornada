namespace Jornada.Repositories;

public interface IDepoimentoFotoRepository
{
    Task AddAsync(int depoimentoId, int fotoId);
}
