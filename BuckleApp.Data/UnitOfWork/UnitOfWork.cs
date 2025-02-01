using BuckleApp.Data.Data;
using BuckleApp.Data.Repositories;
using BuckleApp.Core.IRepositories;
using BuckleApp.Core.IUnitOfWork;

namespace BuckleApp.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationContext _applicationContext;

    private IAppointmentRepository _appointmentRepository;
    private IOrganisationRepository _organisationRepository;
    
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

    public IOrganisationRepository Organisation
    {
        get
        {
            _organisationRepository = new OrganisationRepository(_applicationContext);
            return _organisationRepository;
        }
    }

    public async Task SaveAsync() => await _applicationContext.SaveChangesAsync();
}