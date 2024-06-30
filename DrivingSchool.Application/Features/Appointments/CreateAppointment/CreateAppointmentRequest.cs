using DrivingSchool.Application.Dtos;
using DrivingSchool.Core.Enumerations;

namespace DrivingSchool.Application.Features.Appointments.CreateAppointment;

public class CreateAppointmentRequest
{
    public List<CreateAppointmentDto> Appointments { get; set; }
}