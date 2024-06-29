using DrivingSchool.Application.Dtos;
using DrivingSchool.Core.Entities;
using DrivingSchool.Core.Enumerations;

namespace DrivingSchool.Application.Features.Appointments.GetAllByOrganiserId;

public class GetAllByOrganiserIdResponse
{
    public List<AppointmentDto> Appointments { get; set; }
}