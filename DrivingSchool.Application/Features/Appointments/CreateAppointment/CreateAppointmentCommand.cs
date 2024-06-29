using DrivingSchool.Core.Enumerations;
using MediatR;

namespace DrivingSchool.Application.Features.Appointments.CreateAppointment;

public class CreateAppointmentCommand : IRequest<CreateAppointmentResponse>
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; }
    public AppointmentType Type { get; set; }
    
    public Guid? AttendeeId { get; set; }
}