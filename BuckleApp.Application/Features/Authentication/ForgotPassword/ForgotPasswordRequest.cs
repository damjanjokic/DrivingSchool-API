using System.ComponentModel.DataAnnotations;

namespace BuckleApp.Application.Features.Authentication.ForgotPassword;

public class ForgotPasswordRequest
{
    [Required]
    public string Email { get; set; }
    public string ClientURI { get; set; }
}