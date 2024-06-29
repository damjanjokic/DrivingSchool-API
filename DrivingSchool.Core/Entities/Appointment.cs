using DrivingSchool.Core.Enumerations;

namespace DrivingSchool.Core.Entities;

public class Appointment : BaseModel
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Description { get; set; }
    public AppointmentType Type { get; set; }
    public bool Set { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public ICollection<UserAppointment> UserAppointments { get; set; }
    
}