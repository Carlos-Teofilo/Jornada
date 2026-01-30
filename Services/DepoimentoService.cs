using Jornada.Models;
using Jornada.Repositories;
using Jornada.ViewModels.Depoimentos;

namespace Jornada.Services;

public class DepoimentoService : IDepoimentoService
{
    private readonly IDepoimentoRepository _repository;

    public DepoimentoService(IDepoimentoRepository repository) => _repository = repository;

    public async Task<DetailDepoimentoViewModel> CreateAsync(Usuario usuario, PostDepoimentoViewModel model)
    {
        try
        {
            var result = await _repository.CreateAsync(
                new Depoimento
                    {
                        Descricao = model.Descricao,
                        Foto = model.Foto,
                        Usuario = usuario
                    });
            
            return new DetailDepoimentoViewModel
            {
                Descricao = result.Descricao,
                Foto = result.Foto,
                Usuario = $"{usuario.Nome} ({usuario.Email})" // TODO: Temporario
            };
        }
        catch (Exception ex)
        {
            throw new Exception("", ex);
        }
    }
    public async Task<bool> DeleteAsync(Usuario usuario, int id) => throw new NotImplementedException();
    public async Task<List<ListDepoimentoViewModel>> GetAsync(string? nome, int page, int pageSize) => throw new NotImplementedException();
    public async Task<DetailDepoimentoViewModel> GetByIdAsync(int id) => throw new NotImplementedException();
    public async Task<bool> UpdateAsync(Usuario usuario, PutDepoimentoViewModel model, int id) => throw new NotImplementedException();
}