using MediatR;

namespace DrivingSchool.Application.Features.Appointments.GetAllByOrganiserId;

public class GetAllByOrganiserIdQuery : IRequest<GetAllByOrganiserIdResponse>
{
    public Guid Id { get; set; }
}