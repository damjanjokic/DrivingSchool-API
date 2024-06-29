using DrivingSchool.Core.Entities;
using DrivingSchool.Data.Data;
using Microsoft.AspNetCore.Identity;

namespace DrivingSchool.API.Extensions;

public static class IdentityConfiguration
{
    public static void ConfigureIdentity(this WebApplicationBuilder builder) =>
        builder.Services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

    public static void ConfigureIdentityToken(this WebApplicationBuilder builder) =>
        builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromHours(2));

    /*public static void ConfigureIdentityOps(this WebApplicationBuilder builder)
    {
        var identity = builder.Services.AddIdentityCore<User>(opt =>
        {
            opt.Password.RequireDigit = true;
            opt.Password.RequiredLength = 6;
            opt.Password.RequireNonAlphanumeric = false;
            opt.Password.RequireUppercase = true;
            opt.SignIn.RequireConfirmedEmail = false;
        });
        
        
    }*/
}