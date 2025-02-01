using MediatR;

namespace BuckleApp.Application.Features.Authentication.ForgotPassword;

public class ForgotPasswordCommand : IRequest<ForgotPasswordResponse>
{
    public string Email { get; set; }
    public string ClientURI { get; set; }
}