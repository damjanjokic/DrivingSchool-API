using BuckleApp.Core.Enumerations;
using MediatR;

namespace BuckleApp.Application.Features.Authentication.RegisterUser;

public class RegisterUserCommand : IRequest<RegisterUserResponse>
{
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Guid OrganisationId { get; set; }
    public Role Role { get; set; }
}