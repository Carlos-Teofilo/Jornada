using Jornada.Data;
using Jornada.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada.Repositories;

public class DepoimentoRepository : IDepoimentoRepository
{
    private readonly JornadaDataContext _context;

    public DepoimentoRepository(JornadaDataContext context) => _context = context;

    public async Task<Depoimento> CreateAsync(Depoimento depoimento)
    {
        try {
            await _context.Depoimentos.AddAsync(depoimento);
            await _context.SaveChangesAsync();
            return depoimento;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao salvar o depoimento no banco de dados.", ex);
        }
    }
    
    public async Task<bool> DeleteAsync(Usuario usuario, int id)
    {
        try
        {
            var row = await _context.Depoimentos
                        .Where(x => x.Id == id && x.Usuario.Id == usuario.Id)
                        .ExecuteDeleteAsync();
            return row > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao deletar depoimento do banco de dados.", ex); 
        }
    }

    public async Task<(List<Depoimento>, int Total)> GetAllAsync(int page, int pageSize)
    {
        var total = await _context.Depoimentos.CountAsync();
        var depoimentos = await _context.Depoimentos
                .AsNoTracking()
                .Include(x => x.Usuario)
                .Include(x => x.DepoimentoFotos)
                    .ThenInclude(x => x.Foto)
                .OrderByDescending(x => x.Id)
                .Skip(page * pageSize)
                .Take(pageSize)
                .ToListAsync();
        
        return (depoimentos, total);
    }

    public async Task<Depoimento?> GetByIdAsync(int id)
    {
        var depoimento = await _context.Depoimentos
                .AsNoTracking()
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        
        return depoimento;
    }

    public async Task<bool> UpdateAsync(Usuario usuario, Depoimento depoimento, int id)
    {
        try {
            var row = await _context.Depoimentos
                    .Where(x => x.Id == id && x.Usuario.Id == usuario.Id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(p => p.Descricao, p => depoimento.Descricao ?? p.Descricao)
                    );
            
            return row > 0;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar depoimento", ex);
        }
    }

    public async Task<List<Depoimento>> GetRandom(int take)
    {
        var depoimentos = await _context.Depoimentos
                .AsNoTracking()
                .Include(x => x.Usuario)
                .OrderBy(x => Guid.NewGuid())
                .Take(take)
                .ToListAsync();
        
        return depoimentos;
    }
}