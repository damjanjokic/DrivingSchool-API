using DrivingSchool.Core.Entities;

namespace DrivingSchool.Core.IRepositories;

public interface IAppointmentRepository
{
    Task<Appointment> GetById(Guid id);
    Task<List<Appointment>> GetAllByOrganiserId(Guid userId);
    Task<List<Appointment>> GetNotCanceledByOrganiserId(Guid userId);

    Task<List<Appointment>> GetAllByAttendeeId(Guid userId);
    Task<List<Appointment>> GetNotCanceledByAttendeeId(Guid userId);
    Task CreateAppointment(Appointment appointment);
    Task<bool> IsOverlappingAppointmentAsync(DateTime startTime, DateTime endTime, Guid userId);
    
}