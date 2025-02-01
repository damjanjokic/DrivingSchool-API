using MediatR;

namespace BuckleApp.Application.Features.Organisations.CreateOrganisation;

public class CreateOrganisationCommand : IRequest<CreateOrganisationResponse>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}