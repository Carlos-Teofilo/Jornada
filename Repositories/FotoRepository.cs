using Jornada.Data;
using Jornada.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada.Repositories;

public class FotoRepository : IFotoRepository
{
    private readonly JornadaDataContext _context;

    public FotoRepository(JornadaDataContext context) => _context = context;

    public async Task<Foto> CreateAsync(Foto foto)
    {
        await _context.Fotos.AddAsync(foto);
        await _context.SaveChangesAsync();
        return foto;
    }
    public async Task<Foto?> GetByUrlAsync(string url)
    {
        var foto = await _context.Fotos
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Url == url);

        return foto;
    }
}