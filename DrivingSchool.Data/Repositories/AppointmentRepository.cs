using DrivingSchool.Core.Entities;
using DrivingSchool.Core.IRepositories;
using DrivingSchool.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Repositories;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(ApplicationContext context) : base(context)
    {
    }

    public async Task<Appointment> GetById(Guid id) =>
        await GetAsync(id);

    public async Task<List<Appointment>> GetAllByOrganiserId(Guid userId) =>
        await Context.Appointments
            .Include(x => x.UserAppointments)
            .Where(x => x.UserId == userId)
            .ToListAsync();

    public async Task<List<Appointment>> GetNotCanceledByOrganiserId(Guid userId) =>
        await Context.Appointments
            .Include(x => x.UserAppointments)
            .Where(x => x.UserAppointments.Any(y => !y.isCanceled) && x.UserId == userId)
            .ToListAsync();

    public async Task<List<Appointment>> GetAllByOrganiserIdAndDates(Guid userId, List<DateTime> dates) =>
        await Context.Appointments
            .Include(x => x.User)
            .Include(x => x.UserAppointments)
            .ThenInclude(x => x.User)
            .Where(x => x.UserId == userId && dates.Any(y=> y.Date == x.StartTime.Date))
            .ToListAsync();

    public async Task<List<Appointment>> GetAllByAttendeeId(Guid userId) =>
        await Context.Appointments
            .Include(x => x.UserAppointments)
            .Where(x => x.UserAppointments.Any(a => a.UserId == userId))
            .ToListAsync();

    public async Task<List<Appointment>> GetNotCanceledByAttendeeId(Guid userId) => 
        await Context.Appointments
            .Include(x => x.UserAppointments)
            .Where(x => x.UserAppointments.Any(a => a.UserId == userId && !a.isCanceled))
            .ToListAsync();
    
    public async Task<bool> IsOverlappingAppointmentAsync(DateTime startTime, DateTime endTime, Guid userId) => 
        await Context.Appointments
            .AnyAsync(a => a.UserId == userId && 
                           ((startTime >= a.StartTime && startTime < a.EndTime) || 
                            (endTime > a.StartTime && endTime <= a.EndTime) || 
                            (startTime <= a.StartTime && endTime >= a.EndTime)));


        public async Task CreateAppointments (List<Appointment> appointments) =>
        await AddRange(appointments);
        
    
}