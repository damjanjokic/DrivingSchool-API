using System.Linq.Expressions;
using DrivingSchool.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.Core.IRepositories;

public interface IAuthRepository
{
    Task<IdentityResult> CreateAsync(User user, string password);
    Task<SignInResult> CheckPasswordSignInAsync(User User, string password, bool lockoutOnFailure);
    Task<User> FindByUsernameAsync(string username);
    Task<User> FindByEmailAsync(string username);
    Task UpdateAsync(User user);
    Task<bool> UserExistsQueryAsync(Expression<Func<User, bool>> filter = null);
    Task<string> GenerateEmailConfirmationTokenAsync(User user);
    Task<IdentityResult> ConfirmEmailAsync(User user, string decodedToken);
    Task<string> GeneratePasswordResetTokenAsync(User user);
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
}