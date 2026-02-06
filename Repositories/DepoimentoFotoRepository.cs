
using Jornada.Data;
using Jornada.Models;

namespace Jornada.Repositories;

public class DepoimentoFotoRepository : IDepoimentoFotoRepository
{
    private readonly JornadaDataContext _context;

    public DepoimentoFotoRepository(JornadaDataContext context) => _context = context;

    public async Task AddAsync(int depoimentoId, int fotoId)
    {
        var link = new DepoimentoFoto
        {
            DepoimentoId = depoimentoId,
            FotoId = fotoId
        };

        await _context.DepoimentoFotos.AddAsync(link);
        await _context.SaveChangesAsync();
    }
}