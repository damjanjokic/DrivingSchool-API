using DrivingSchool.Core.Entities;
using DrivingSchool.Core.IRepositories;
using DrivingSchool.Core.IUnitOfWork;
using DrivingSchool.Data.Repositories;
using DrivingSchool.Data.UnitOfWork;
using DrivingSchool.Infrastructure.Interfaces;
using DrivingSchool.Infrastructure.Security;

namespace DrivingSchool.API.Extensions;

public static class DependencyContainer
{
    public static void ResolveDependenciesExtension(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IJwtGenerator, JwtGenerator>();
        builder.Services.AddScoped<IAuthRepository, AuthRepository>();
        builder.Services.AddScoped<IJwtHandler, JwtHandler>();
        builder.Services.AddScoped<IUserAccessor, UserAccessor>();
        
        builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}