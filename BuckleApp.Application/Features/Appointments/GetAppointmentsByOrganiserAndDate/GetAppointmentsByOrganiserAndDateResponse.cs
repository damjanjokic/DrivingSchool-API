using BuckleApp.Application.Dtos;

namespace BuckleApp.Application.Features.Appointments.GetAppointmentsByOrganiserAndDate;

public class GetByOrganiserAndDateResponse
{
    public List<GetAppointmentInfoDto> Appointments { get; set; }
}