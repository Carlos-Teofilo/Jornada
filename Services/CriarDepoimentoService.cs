using Jornada.Models;
using Jornada.Repositories;
using Jornada.ViewModels.Depoimentos;

namespace Jornada.Services;

public class CriarDepoimentoService : ICriarDepoimentoService
{
    private readonly IDepoimentoRepository _depoimentoRepo;
    private readonly IFotoRepository _fotoRepo;
    private readonly IDepoimentoFotoRepository _depoimentoFotoRepo;

    public CriarDepoimentoService(
        IDepoimentoRepository depoimentoRepo,
        IFotoRepository fotoRepo,
        IDepoimentoFotoRepository depoimentoFotoRepo
    )
    {
        _depoimentoRepo = depoimentoRepo;
        _fotoRepo = fotoRepo;
        _depoimentoFotoRepo = depoimentoFotoRepo;
    }

    public async Task<DetailDepoimentoViewModel> ExecuteAsync(
        Usuario usuario,
        PostDepoimentoViewModel model
        )
    {
        var depoimento = new Depoimento
        {
            Descricao = model.Descricao,
            Usuario = usuario
        };

        await _depoimentoRepo.CreateAsync(depoimento);

        foreach (var url in model.Fotos)
        {
            var foto = await _fotoRepo.GetByUrlAsync(url)
                        ?? await _fotoRepo.CreateAsync(new Foto { Url = url});

            await _depoimentoFotoRepo.AddAsync(depoimento.Id, foto.Id);
        }

        return new DetailDepoimentoViewModel
        {
            Id = depoimento.Id,
            Descricao = depoimento.Descricao,
            Usuario = $"{usuario.Nome} - ({usuario.Email})"
        };
    }
}