using System.Data.Common;
using Jornada.Data;
using Jornada.Models;
using Jornada.ViewModels.Depoimentos;
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
            return row > 1;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao deletar depoimento.", ex); 
        }
    }

    public async Task<(List<Depoimento>, int Total)> GetAllAsync(int page = 0, int pageSize = 25)
    {
        var total = await _context.Depoimentos.CountAsync();
        var depoimentos = await _context.Depoimentos
                .AsNoTracking()
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
                        .SetProperty(p => p.Foto, p => depoimento.Foto ?? p.Foto)
                    );
            
            return row > 1;
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao atualizar depoimento", ex);
        }
    }
}