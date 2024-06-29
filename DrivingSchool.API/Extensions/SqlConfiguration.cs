using DrivingSchool.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.API.Extensions;

public static class SqlConfiguration
{
    public static void ConfigureSqlContext(this WebApplicationBuilder builder) =>
        builder.Services.AddDbContext<ApplicationContext>(opts =>
            opts.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));
}