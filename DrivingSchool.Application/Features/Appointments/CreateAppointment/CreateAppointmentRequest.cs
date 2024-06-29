using DrivingSchool.Core.Enumerations;

namespace DrivingSchool.Application.Features.Appointments.CreateAppointment;

public class CreateAppointmentRequest
{
    [ValidAppointmentTimes]
    public DateTime StartTime { get; set; }
    [ValidAppointmentTimes]
    public DateTime EndTime { get; set; }
    public string Description { get; set; }
    public AppointmentType Type { get; set; }
    
    public Guid? AttendeeId { get; set; }
}