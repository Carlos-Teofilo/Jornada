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
    private readonly IDepoimentoService _depoimentoService;
    private readonly IUsuarioService _usuarioService;
    private readonly ICriarDepoimentoService _criarDepoimentoService;

    public DepoimentoController(
        IDepoimentoService depoimentoService,
        IUsuarioService usuarioService,
        ICriarDepoimentoService criarDepoimentoService
    )
    {
        _depoimentoService = depoimentoService;
        _usuarioService = usuarioService;
        _criarDepoimentoService = criarDepoimentoService;
    }
    
    [HttpGet("depoimentos")]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int page = 0,
        [FromQuery] int pageSize = 25
    )
    {
        var depoimentos = await _depoimentoService.GetAsync(page, pageSize);

        return Ok(new ResultViewModel<ListDepoimentoViewModel>(depoimentos, null));
    }

    [HttpGet("depoimentos/{id:int}")]
    public async Task<IActionResult> DetailAsync(
        [FromRoute] int id
    )
    {
        var depoimento = await _depoimentoService.GetByIdAsync(id);

        if (depoimento is null)
            return NotFound();

        return Ok(new ResultViewModel<DetailDepoimentoViewModel>(depoimento, null));
    }

    [Authorize]
    [HttpPut("depoimentos/{id:int}")]
    public async Task<IActionResult> PutAsync(
        [FromRoute] int id,
        [FromBody] PutDepoimentoViewModel model
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        var email = User.Identity?.Name;

        if (email is null)
            return Unauthorized();

        var usuario = await GetLoggedUserAsync();

        if (usuario is null)
            return Unauthorized();

        return await _depoimentoService.UpdateAsync(usuario, model, id) ? NoContent() : NotFound();
    }

    [Authorize]
    [HttpPost("depoimentos")]
    public async Task<IActionResult> PostAsync(
        [FromBody] PostDepoimentoViewModel model
    )
    {

        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));

        if (model is null)
            return BadRequest();

        var email = User.Identity?.Name;

        if (email is null)
            return Unauthorized();

        var usuario = await GetLoggedUserAsync();

        if (usuario is null)
            return Unauthorized();

        var result = await _criarDepoimentoService.ExecuteAsync(usuario, model);

        return Created($"api/v1/depoimentos/{result.Id}", result);
    }

    [Authorize]
    [HttpDelete("depoimentos/{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id
    )
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var email = User.Identity?.Name;

        if (email is null)
            return Unauthorized();

        var usuario = await GetLoggedUserAsync();

        if (usuario is null)
            return Unauthorized();

        return await _depoimentoService.DeleteAsync(usuario, id) ? NoContent() : NotFound(
            new ResultViewModel<string>("Depoimento não encontrado")
            );
    }

    [HttpGet("depoimentos-home")]
    public async Task<IActionResult> GetDepoimentosHomeAsync(
        [FromQuery] int take
    )
    {
        var depoimentos = await _depoimentoService.GetRandom(take);
        
        return Ok(new ResultViewModel<ListDepoimentoViewModel>(depoimentos, null));
    }

    private async Task<Usuario?> GetLoggedUserAsync()
    {
        var email = User.Identity?.Name;
        if (string.IsNullOrEmpty(email)) return null;
        return await _usuarioService.GetByEmail(email);
    }
}