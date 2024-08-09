﻿using DrivingSchool.Core.Entities;
using DrivingSchool.Core.Enumerations;

namespace DrivingSchool.Application.Dtos;

public class GetAppointmentDto
{
    public Guid Id { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public string Note { get; set; }
    public AppointmentType Type { get; set; }
    public bool Set { get; set; }
    
    public User User { get; set; }
    
    public ICollection<UserAppointment> UserAppointments { get; set; }
}