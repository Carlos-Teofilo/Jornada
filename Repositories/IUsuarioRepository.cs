using Jornada.Models;

namespace Jornada.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario> CreateAsync(Usuario usuario);
    Task<bool> DeleteAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<bool> ExistsByEmailAsync(string email);
}