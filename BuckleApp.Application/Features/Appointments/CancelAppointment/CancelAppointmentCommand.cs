using MediatR;

namespace BuckleApp.Application.Features.Appointments.CancelAppointment;

public class CancelAppointmentCommand : IRequest<CancelAppointmentResponse>
{
    public Guid Id { get; set; }
    public string Reason { get; set; }
}