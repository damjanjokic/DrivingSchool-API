using BuckleApp.Application.Dtos;
using BuckleApp.Core.Enumerations;
using MediatR;

namespace BuckleApp.Application.Features.Appointments.CreateAppointment;

public class CreateAppointmentCommand : IRequest<CreateAppointmentResponse>
{
    public List<CreateAppointmentDto> Appointments { get; set; }
}