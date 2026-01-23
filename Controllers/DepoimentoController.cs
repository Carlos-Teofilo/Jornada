using Azure;
using Jornada.Data;
using Jornada.Extensions;
using Jornada.Models;
using Jornada.Services;
using Jornada.ViewModels;
using Jornada.ViewModels.Depoimentos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jornada.Controllers;

[ApiController]
[Route("api/v1")]
public class DepoimentoController : ControllerBase
{
    [HttpGet("depoimentos")]
    public async Task<IActionResult> GetAsync(
        [FromServices] JornadaDataContext context,
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25
    )
    {
        try{
            var total = await context.Depoimentos.CountAsync();
            var depoimentos = await context.Depoimentos
                                        .AsNoTracking()
                                        .OrderByDescending(x => x.Id)
                                        .Skip(page * pageSize)
                                        .Take(pageSize)
                                        .Select(x => new ListDepoimentoViewModel
                                        {
                                            Id = x.Id,
                                            Descricao = x.Descricao,
                                            Foto = x.Foto,
                                        })
                                        .ToListAsync();

            return Ok(new ResultViewModel<dynamic>(new
            {
                Depoimentos = depoimentos,
                total,
                page,
                pageSize
            }, null));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno no servidor!"));
        }
    }

    [HttpGet("depoimentos/{id:int}")]
    public async Task<IActionResult> DetailAsync(
        [FromRoute] int id,
        [FromServices] JornadaDataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var depoimento = await context.Depoimentos
                            .AsNoTracking()
                            .Select(detail => new DetailDepoimentoViewModel {
                                Id = detail.Id,
                                Descricao = detail.Descricao,
                                Foto = detail.Foto,
                                Usuario = $"{detail.Usuario.Nome} ({detail.Usuario.Email})"
                                })
                            .FirstOrDefaultAsync(x => x.Id == id);

        if (depoimento is null)
            return NotFound();

        return Ok(new ResultViewModel<DetailDepoimentoViewModel>(depoimento, null));
    }

    [Authorize]
    [HttpPut("depoimentos/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] PutDepoimentoViewModel model,
        [FromServices] JornadaDataContext context
    )
    {
        var row = await context.Depoimentos
            .Where(x => x.Id == id && x.Usuario.Email == User.Identity.Name)
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.Descricao, p => model.Descricao ?? p.Descricao)
                .SetProperty(p => p.Foto, p => model.Foto ?? p.Foto)
                );
        
        return row != 0 ? NoContent() : NotFound();
    }

    [Authorize]
    [HttpPost("depoimentos")]
    public async Task<IActionResult> PostAsync(
        [FromBody] PostDepoimentoViewModel model,
        [FromServices] JornadaDataContext context
    )
    {

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
        if (model is null)
            return BadRequest();

        var usuario = await context.Usuarios
                            .FirstOrDefaultAsync(x => x.Email == User.Identity.Name);
        
        if (usuario is null)
            return NotFound(new ResultViewModel<string>("Usuário não encontrado!"));
        
        var depoimento = new Depoimento
        {
            Id = 0,
            Descricao = model.Descricao,
            Foto = model.Foto,
        };

        depoimento.Usuario = usuario;

        try
        {
            await context.Depoimentos.AddAsync(depoimento);
            await context.SaveChangesAsync();

            return Created($"depoimentos/{depoimento.Id}", new ResultViewModel<DetailDepoimentoViewModel>(
                new DetailDepoimentoViewModel
                {
                    Id = depoimento.Id,
                    Descricao = depoimento.Descricao,
                    Foto = depoimento.Foto,
                    Usuario = $"{usuario.Nome} ({usuario.Email})"
                }, null));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<string>("05X11 - Não foi possível salvar o depoimento."));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>("05X12 - Erro interno no servidor."));
        }
    }

    [Authorize]
    [HttpDelete("depoimentos/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] JornadaDataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var row = await context.Depoimentos
                                    .Where(x =>
                                    x.Id == id &&
                                    x.Usuario.Email == User.Identity.Name
                                    ).ExecuteDeleteAsync();
        
        return row != 0 ? NoContent() : NotFound(
            new ResultViewModel<string>("Depoimento não encontrado")
            );
    }

    [HttpGet("depoimentos-home")]
    public async Task<IActionResult> GetDepoimentosHomeAsync(
        [FromServices] JornadaDataContext context
    )
    {
        var depoimentos = await context.Depoimentos
                            .AsNoTracking()
                            .OrderBy(x => Guid.NewGuid())
                            .Take(3)
                            .Select(x => new DetailDepoimentoViewModel
                                    {
                                        Id = x.Id,
                                        Descricao = x.Descricao,
                                        Foto = x.Foto,
                                        Usuario = $"{x.Usuario.Nome} ({x.Usuario.Email})"
                                    })
                            .ToListAsync();
        
        return Ok(new ResultViewModel<List<DetailDepoimentoViewModel>>(depoimentos, null));
    }
}