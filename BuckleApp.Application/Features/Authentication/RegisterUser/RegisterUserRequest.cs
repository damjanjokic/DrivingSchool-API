using System.ComponentModel.DataAnnotations;
using BuckleApp.Core.Enumerations;

namespace BuckleApp.Application.Features.Authentication.RegisterUser;

public class RegisterUserRequest
{
    
    [Required] public string Username { get; set; }

    [Required]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    [MaxLength(256, ErrorMessage = "Password must be less than 256 characters long.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    
    [Required] public string PhoneNumber { get; set; }

    [Required] public string FirstName { get; set; }

    [Required] public string LastName { get; set; }
    
    [Required] public DateTime Birthday { get; set; }
    [Required]
    public Guid OrganisationId { get; set; }

    public Role Role { get; set; }
}