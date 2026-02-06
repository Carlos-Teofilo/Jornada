using Jornada.Models;
using Jornada.ViewModels.UsuarioViewModel;

namespace Jornada.Services;

public interface IUsuarioService
{
    Task<Usuario?> GetByEmail(string email);
}