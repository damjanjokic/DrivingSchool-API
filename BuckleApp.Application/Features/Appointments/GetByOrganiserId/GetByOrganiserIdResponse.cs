using BuckleApp.Application.Dtos;

namespace BuckleApp.Application.Features.Appointments.GetByOrganiserId;

public class GetAllByOrganiserIdResponse
{
    public List<GetAppointmentDto> Appointments { get; set; }
}