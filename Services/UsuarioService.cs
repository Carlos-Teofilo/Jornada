using Jornada.Models;
using Jornada.Repositories;
using Jornada.ViewModels.UsuarioViewModel;

namespace Jornada.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository) => _repository = repository;

    public async Task<Usuario?> GetByEmail(string email)
    {
        var usuario = await _repository.GetByEmailAsync(email);
        
        return usuario ?? null;
    }
}