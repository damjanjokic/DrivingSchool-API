using MediatR;

namespace BuckleApp.Application.Features.Appointments.DeleteAppointment;

public class DeleteAppointmentCommand : IRequest<DeleteAppointmentResponse>
{
    public Guid Id { get; set; }
}