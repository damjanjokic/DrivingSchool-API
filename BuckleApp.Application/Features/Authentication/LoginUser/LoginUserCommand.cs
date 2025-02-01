using MediatR;

namespace BuckleApp.Application.Features.Authentication.LoginUser;

public class LoginUserCommand : IRequest<LoginUserResponse>
{
    public string Email { get; set; }
    public string Password { get; set; }
}