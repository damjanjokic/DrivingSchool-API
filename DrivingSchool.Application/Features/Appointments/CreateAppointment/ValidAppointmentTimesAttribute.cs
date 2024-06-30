using System.ComponentModel.DataAnnotations;
using DrivingSchool.Application.Dtos;

namespace DrivingSchool.Application.Features.Appointments.CreateAppointment;

public class ValidAppointmentTimesAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var appointment = (CreateAppointmentDto)validationContext.ObjectInstance;

        if (appointment.StartTime <= DateTime.Now)
        {
            return new ValidationResult("Start time must be greater than the current time.");
        }

        if (appointment.EndTime <= DateTime.Now)
        {
            return new ValidationResult("End time must be greater than the current time.");
        }

        if (appointment.EndTime <= appointment.StartTime)
        {
            return new ValidationResult("End time must be greater than start time.");
        }

        return ValidationResult.Success;
    }
}