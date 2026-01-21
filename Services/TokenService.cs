using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jornada.Extensions;
using Jornada.Models;
using Microsoft.IdentityModel.Tokens;

namespace Jornada.Services;

public class TokenService
{
    public string GenerateToken(Usuario usuario)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
        var claims = usuario.GetClaims();
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Expires = DateTime.UtcNow.AddHours(8)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}