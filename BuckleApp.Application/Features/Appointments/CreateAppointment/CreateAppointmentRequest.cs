using BuckleApp.Application.Dtos;
using BuckleApp.Core.Enumerations;

namespace BuckleApp.Application.Features.Appointments.CreateAppointment;

public class CreateAppointmentRequest
{
    public List<CreateAppointmentDto> Appointments { get; set; }
}