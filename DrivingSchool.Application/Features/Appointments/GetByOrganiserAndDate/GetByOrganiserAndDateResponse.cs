using DrivingSchool.Application.Dtos;

namespace DrivingSchool.Application.Features.Appointments.GetByOrganiserAndDate;

public class GetByOrganiserAndDateResponse
{
    public List<GetAppointmentInfoDto> Appointments { get; set; }
}