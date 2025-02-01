﻿using System.Linq.Expressions;
using BuckleApp.Core.Entities;
using BuckleApp.Core.Enumerations;
using Microsoft.AspNetCore.Identity;

namespace BuckleApp.Core.IRepositories;

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
    Task<bool> AddToRoleAsync(User user, Role role);
    Task<IEnumerable<string>> GetUserRoles(User user);
}