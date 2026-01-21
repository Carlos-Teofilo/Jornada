using Jornada.Data;
using Jornada.Extensions;
using Jornada.Models;
using Jornada.Services;
using Jornada.ViewModels;
using Jornada.ViewModels.UsuarioViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace Jornada.Controllers;

[ApiController]
[Route("api/v1")]
public class UsuarioController : ControllerBase
{
    [HttpPost("account/register")]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterUsuarioViewModel model,
        [FromServices] JornadaDataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

        var usuario = new Usuario
        {
            Nome = model.Nome,
            Email = model.Email
        };
        var senha = PasswordHasher.Hash(model.Senha);
        usuario.SenhaHash = senha;

        try
        {
            await context.Usuarios.AddAsync(usuario);
            await context.SaveChangesAsync();

            return Created("", new ResultViewModel<string>("Usuário criado com sucesso!", null));
        }
        catch (DbUpdateException)
        {
            return StatusCode(400, new ResultViewModel<string>("Este email já está cadastrado!"));
        }
    }

    [HttpPost("account/login")]
    public async Task<IActionResult> LoginAsync(
        [FromBody] LoginUsuarioViewModel model,
        [FromServices] TokenService tokenService,
        [FromServices] JornadaDataContext context
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResultViewModel<List<string>>(ModelState.GetErrors()));
    
        var usuario = await context.Usuarios
                        .AsNoTracking()
                        .Include(x => x.Roles)
                        .FirstOrDefaultAsync(x => x.Email == model.Email);

        if (usuario is null)
            return NotFound(new ResultViewModel<Usuario>("Email e/ou senha estão incorretos"));

        var senha = PasswordHasher.Verify(usuario.SenhaHash, model.Senha);

        if (senha is false)
            return NotFound(new ResultViewModel<Usuario>("Email e/ou senha estão incorretos"));
        
        var token = tokenService.GenerateToken(usuario);

        return Ok(token);
    }
}