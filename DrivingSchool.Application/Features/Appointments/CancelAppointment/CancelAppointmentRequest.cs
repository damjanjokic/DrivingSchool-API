using System.ComponentModel.DataAnnotations;

namespace DrivingSchool.Application.Features.Appointments.CancelAppointment;

public class CancelAppointmentRequest
{
    [Required]
    public string Reason { get; set; }
}