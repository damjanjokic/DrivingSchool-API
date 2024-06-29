using DrivingSchool.Core.Entities;

namespace DrivingSchool.Infrastructure.Interfaces;

public interface IJwtGenerator
{
    string CreateToken(User user);
}