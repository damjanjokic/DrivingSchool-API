using MediatR;

namespace DrivingSchool.Application.Features.Appointments.GetByOrganiserAndDate;

public class GetByOrganiserAndDateQuery : IRequest<GetByOrganiserAndDateResponse>
{
    public Guid OrganiserId { get; set; }
    public List<DateTime> Dates { get; set; }
}