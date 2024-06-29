using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Application.Features.Authentication.LoginUser;

public class LoginUserRequest
{
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}