using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuckleApp.Infrastructure.Interfaces;
using BuckleApp.Core.Entities;
using BuckleApp.Core.Enumerations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BuckleApp.Infrastructure.Security;

public class JwtGenerator : IJwtGenerator
{
    private readonly SymmetricSecurityKey _key;
    public JwtGenerator(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("Token").Value));
    }
    public string CreateToken(User user, List<string> roles)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        };
        
        claims.AddRange(roles.Select(x => new Claim(ClaimsIdentity.DefaultRoleClaimType, x)));
        
        // generate signing credentials
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}