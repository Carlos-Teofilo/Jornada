using System.Security.Claims;
using Jornada.Models;

namespace Jornada.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this Usuario usuario)
    {
        var result = new List<Claim>
        {
            new Claim(ClaimTypes.Name, usuario.Email),
        };
        
        result.AddRange(usuario.Roles.Select(
            role => new Claim(ClaimTypes.Role, role.Slug)
            ));

        return result;
    }
}