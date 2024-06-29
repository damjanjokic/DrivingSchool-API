using DrivingSchool.Core.IRepositories;

namespace DrivingSchool.Core.IUnitOfWork;

public interface IUnitOfWork
{
    IAppointmentRepository Appointment { get; }
    Task SaveAsync();
}