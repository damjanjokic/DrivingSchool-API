﻿using DrivingSchool.Core.Entities;

namespace DrivingSchool.Core.IRepositories;

public interface IAppointmentRepository : IRepository<Appointment>
{
    Task<Appointment> GetById(Guid id, bool includeUserAppointment = false);
    Task<List<Appointment>> GetAllByOrganiserId(Guid userId);
    Task<List<Appointment>> GetNotCanceledByOrganiserId(Guid userId);
    Task<List<Appointment>> GetAllByOrganiserIdAndDates(Guid userId, List<DateTime> dates);

    Task<List<Appointment>> GetAllByAttendeeId(Guid userId);
    Task<List<Appointment>> GetNotCanceledByAttendeeId(Guid userId);
    Task CreateAppointments (List<Appointment> appointments);
    Task<bool> IsOverlappingAppointmentAsync(DateTime startTime, DateTime endTime, Guid userId);
    
}