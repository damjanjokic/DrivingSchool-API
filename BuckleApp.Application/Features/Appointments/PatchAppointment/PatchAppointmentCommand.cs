using BuckleApp.Core.Enumerations;
using MediatR;

namespace BuckleApp.Application.Features.Appointments.PatchAppointment;

public class PatchAppointmentCommand : IRequest<PatchAppointmentResponse>
{
    public Guid Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Note { get; set; }
    public AppointmentType Type { get; set; }
}