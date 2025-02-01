using MediatR;

namespace BuckleApp.Application.Features.Appointments.GetAppointmentsByOrganiserAndDate;

public class GetByOrganiserAndDateQuery : IRequest<GetByOrganiserAndDateResponse>
{
    public Guid OrganiserId { get; set; }
    public List<DateTime> Dates { get; set; }
}