using System.Linq.Expressions;
using BuckleApp.Data.Data;
using BuckleApp.Core.Entities;
using BuckleApp.Core.Enumerations;
using BuckleApp.Core.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BuckleApp.Data.Repositories;

public class AuthRepository : IAuthRepository
{
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ApplicationContext _context;

        public AuthRepository(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure)
        {
            return _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string decodedToken)
        {
            return await _userManager.ConfirmEmailAsync(user, decodedToken);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<User> FindByUsernameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await _userManager.ResetPasswordAsync(user, token, password);
        }

        public Task UpdateAsync(User user)
        {
            return _userManager.UpdateAsync(user);
        }

        public async Task<bool> UserExistsQueryAsync(Expression<Func<User, bool>> filter = null)
        {
            if (await _context.Users.AnyAsync(filter))
                return true;

            return false;
        }

        public async Task<bool> AddToRoleAsync(User user, Role role)
        {
            var result = await _userManager.AddToRoleAsync(user, role.ToString());
            return result.Succeeded;
        }

        public async Task<IEnumerable<string>> GetUserRoles(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
}