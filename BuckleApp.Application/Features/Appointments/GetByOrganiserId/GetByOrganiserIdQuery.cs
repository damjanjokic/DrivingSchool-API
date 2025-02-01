using MediatR;

namespace BuckleApp.Application.Features.Appointments.GetByOrganiserId;

public class GetAllByOrganiserIdQuery : IRequest<GetAllByOrganiserIdResponse>
{
    public Guid Id { get; set; }
}