using DrivingSchool.Application.Features.Appointments.CreateAppointment;
using DrivingSchool.Core.Enumerations;

namespace DrivingSchool.Application.Dtos;

public class CreateAppointmentDto
{
    [ValidAppointmentTimes]
    public DateTime StartTime { get; set; }
    [ValidAppointmentTimes]
    public DateTime EndTime { get; set; }
    public string Note { get; set; }
    public AppointmentType Type { get; set; }
    
    public Guid? AttendeeId { get; set; }
}