using DrivingSchool.Application.Dtos;
using DrivingSchool.Core.Enumerations;
using MediatR;

namespace DrivingSchool.Application.Features.Appointments.CreateAppointment;

public class CreateAppointmentCommand : IRequest<CreateAppointmentResponse>
{
    public List<CreateAppointmentDto> Appointments { get; set; }
}