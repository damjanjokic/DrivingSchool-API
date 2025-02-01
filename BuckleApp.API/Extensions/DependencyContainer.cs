using BuckleApp.Core.Entities;
using BuckleApp.Core.IRepositories;
using BuckleApp.Core.IUnitOfWork;
using BuckleApp.Data.Repositories;
using BuckleApp.Data.UnitOfWork;
using BuckleApp.Infrastructure.Email;
using BuckleApp.Infrastructure.Interfaces;
using BuckleApp.Infrastructure.Security;

namespace BuckleApp.API.Extensions;

public static class DependencyContainer
{
    public static void ResolveDependenciesExtension(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IJwtHandler, JwtHandler>();
        builder.Services.AddScoped<IUserAccessor, UserAccessor>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}