using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DrivingSchool.Core.Entities;
using Microsoft.IdentityModel.Tokens;

namespace DrivingSchool.Infrastructure.Interfaces;

public interface IJwtHandler
{
    SigningCredentials GetSigningCredentials();
    List<Claim> GetClaims(User user);
    JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims);
}