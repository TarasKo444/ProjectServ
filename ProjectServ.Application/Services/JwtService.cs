using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ProjectServ.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProjectServ.Application.Common;

namespace ProjectServ.Application.Services;

public class JwtService(
    IOptions<MyOptions> options)
{
    public async Task<string> GenerateJwtToken(AppUser user)
    {
        var claims = new Claim[]
        {
            new (JwtRegisteredClaimNames.NameId, user.Id.ToString()),
            new (JwtRegisteredClaimNames.Email, user.Email!),
            new (ClaimTypes.Role, user.Role.ToString()),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
            
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.SECRET_KEY));

        var tokenObject = new JwtSecurityToken(
            issuer: options.Value.JWT_ISSUER,
            expires: DateTime.Now.AddDays(60),
            claims: claims,
            signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
        );

        string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);

        return token;
    }
}