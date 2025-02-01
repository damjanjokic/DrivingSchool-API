using BuckleApp.Core.Entities;
using BuckleApp.Core.Enumerations;

namespace BuckleApp.Infrastructure.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(User user, List<string> roles);
}