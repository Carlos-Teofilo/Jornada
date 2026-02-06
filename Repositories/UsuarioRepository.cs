using Jornada.Data;
using Jornada.Models;
using Microsoft.EntityFrameworkCore;

namespace Jornada.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly JornadaDataContext _context;

    public UsuarioRepository(JornadaDataContext context) => _context = context;

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var row = await _context.Usuarios
                    .Where(x => x.Id == id)
                    .ExecuteDeleteAsync();
        
        return row > 0;
    }

    public async Task<Usuario?> GetByEmailAsync(string email)
    {
        var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(x => x.Email == email);
        
        return usuario;
    }
    
    public async Task<bool> ExistsByEmailAsync(string email)
    {
        return await _context.Usuarios
                .AsNoTracking()
                .AnyAsync(x => x.Email == email);
    }
}