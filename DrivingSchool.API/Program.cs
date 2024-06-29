using DrivingSchool.API.Extensions;
using DrivingSchool.API.Middleware;
using DrivingSchool.Application.Features.Authentication.LoginUser;
using DrivingSchool.Application.Mapping;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ResolveDependenciesExtension();
builder.ConfigureIdentity();
builder.ConfigureIdentityToken();
builder.ConfigureSqlContext();

builder.Services.AddMediatR(typeof(LoginUserHandler).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});



//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseCors("AllowSpecificOrigins");
app.UseMiddleware<ErrorHandlingMiddleware>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
