using MediatR;

namespace DrivingSchool.Application.Features.Appointments.DeleteAppointment;

public class DeleteAppointmentCommand : IRequest<DeleteAppointmentResponse>
{
    public Guid Id { get; set; }
}