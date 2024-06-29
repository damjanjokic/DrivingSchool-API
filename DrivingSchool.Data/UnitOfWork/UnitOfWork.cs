using DrivingSchool.Core.IRepositories;
using DrivingSchool.Core.IUnitOfWork;
using DrivingSchool.Data.Data;
using DrivingSchool.Data.Repositories;

namespace DrivingSchool.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _applicationContext;

    private IAppointmentRepository _appointmentRepository;
    
    public UnitOfWork(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
    
    public IAppointmentRepository Appointment 
    {
        get
        {
            _appointmentRepository = new AppointmentRepository(_applicationContext);
            return _appointmentRepository;
        } 
    }

    public async Task SaveAsync() => await _applicationContext.SaveChangesAsync();
}