using System.Data.Common;
using Jornada.Data;
using Jornada.Extensions;
using Jornada.Models;
using Jornada.ViewModels;
using Jornada.ViewModels.Destinos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jornada.Controllers;

[ApiController]
[Route("api/v1")]
public class DestinoController : ControllerBase
{
    [HttpGet("destinos")]
    public async Task<IActionResult> GetAsync(
        [FromServices] JornadaDataContext context,
        [FromQuery] string? nome = "",
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25
    )
    {

        var query = context.Destinos.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(nome))
            query = query.Where(x => x.Nome.Contains(nome));
            
        var total = await query.CountAsync();

        if (total == 0)
        {
            return Ok(new ResultViewModel<dynamic>(new {
                Destinos = new List<Destino>(),
                total = 0,
                page,
                pageSize,
                message = "Nenhum destino foi encontrado"
            }, null));
        }

        var destinos = await query
                            .OrderByDescending(x => x.Id)
                            .Skip(page * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
        
        return Ok(new ResultViewModel<dynamic>(new
            {
                Destinos = destinos,
                total,
                page,
                pageSize
            }, null));
    }

    [HttpGet("destinos/{id:int}")]
    public async Task<IActionResult> DetailAsync(
        [FromRoute] int id,
        [FromServices] JornadaDataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
        
        var destino = await context.Destinos
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.Id == id);

        if (destino is null)
            return NotFound();

        return Ok(new ResultViewModel<Destino>(destino, null));
    }

    [HttpPost("destinos")]
    public async Task<IActionResult> PostAsync(
        [FromBody] PostDestinoViewModel model,
        [FromServices] JornadaDataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
        if (model is null)
            return BadRequest();
            
        var destino = new Destino
        {
            Id = 0,
            Nome = model.Nome,
            Preco = model.Preco,
            Foto = model.Foto
        };

        try
        {
            await context.Destinos.AddAsync(destino);
            await context.SaveChangesAsync();
            return Created($"destinos/{destino.Id}", new ResultViewModel<Destino>(destino, null));
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro ao salvar destino!"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno do servidor!"));
        }       
    }

    [HttpPut("destinos/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] PutDestinoViewModel model,
        [FromServices] JornadaDataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
        
        try
        {
            var row = await context.Destinos
                                    .Where(x => x.Id == id)
                                    .ExecuteUpdateAsync(s => s
                                        .SetProperty(p => p.Nome, p => model.Nome ?? p.Nome)
                                        .SetProperty(p => p.Preco, p => model.Preco ?? p.Preco)
                                        .SetProperty(p => p.Foto, p => model.Foto ?? p.Foto)
                                    );
            
            return row != 0 ? NoContent() : NotFound(
                new ResultViewModel<string>("Destino não encontrado!")
                );
        }
        catch (DbUpdateException)
        {
            return StatusCode(500, new ResultViewModel<string>("Não foi possível atualizar!"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno no servidor!"));
        }
    }

    [HttpDelete("destinos/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] JornadaDataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
        
        try {
            var row = await context.Destinos
                            .Where(x => x.Id == id)
                            .ExecuteDeleteAsync();
            
            return row != 0 ? NoContent() : NotFound(
                new ResultViewModel<string>("Destino não encontrado!")
                );
        }
        catch (DbException)
        {
            return StatusCode(500, new ResultViewModel<string>("Não foi possível deletar!"));
        }
        catch (Exception)
        {
            return StatusCode(500, new ResultViewModel<string>("Erro interno no servidor!"));
        }
    }
}
