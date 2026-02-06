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
                        Usuario = usuario
                    });
            
            return new DetailDepoimentoViewModel
            {
                Descricao = result.Descricao,
                Usuario = usuario != null 
                        ? $"{usuario.Nome} - ({usuario.Email})" 
                        : "Usuário desconhecido"
            };
        }
        catch (Exception ex)
        {
            throw new Exception("", ex);
        }
    }
    public async Task<bool> DeleteAsync(Usuario usuario, int id) => await _repository.DeleteAsync(usuario, id);
    
    public async Task<ListDepoimentoViewModel> GetAsync(int page, int pageSize)
    {
        var (depoimentos, total) = await _repository.GetAllAsync(page, pageSize);
        var depoimentosViewModel = depoimentos.Select(x => new DetailDepoimentoViewModel
        {
            Id = x.Id,
            Descricao = x.Descricao,
            Usuario = x.Usuario != null 
                        ? $"{x.Usuario.Nome} - ({x.Usuario.Email})" 
                        : "Usuário desconhecido"
        }).ToList();

        return new ListDepoimentoViewModel
        {
            Depoimentos = depoimentosViewModel,
            Total = total,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<DetailDepoimentoViewModel?> GetByIdAsync(int id)
    {
        var depoimento = await _repository.GetByIdAsync(id);

        if (depoimento is null)
            return null;

        var depoimentoViewModel = new DetailDepoimentoViewModel
        {
            Id = depoimento.Id,
            Descricao = depoimento.Descricao,
            // Foto = depoimento.Foto,
            Usuario = depoimento.Usuario != null 
                        ? $"{depoimento.Usuario.Nome} - ({depoimento.Usuario.Email})" 
                        : "Usuário desconhecido"
        };

        return depoimentoViewModel;
    }

    public async Task<bool> UpdateAsync(Usuario usuario, PutDepoimentoViewModel model, int id)
    {
        var depoimento = new Depoimento
        {
            Id = id,
            Descricao = model.Descricao,
        };

        return await _repository.UpdateAsync(usuario, depoimento, id);
    }

    public async Task<ListDepoimentoViewModel> GetRandom(int take)
    {
        var depoimentos = await _repository.GetRandom(take);
        var depoimentosViewModel = depoimentos
                .Select(x => new DetailDepoimentoViewModel
                {
                    Id = x.Id,
                    Descricao = x.Descricao,
                }).ToList();
                
        
        return new ListDepoimentoViewModel
        {
            Depoimentos = depoimentosViewModel,
            Total = take
        };
    }
}