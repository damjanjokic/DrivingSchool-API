using System.Net.Mime;
using DrivingSchool.Core.Entities;
using DrivingSchool.Core.Enumerations;
using DrivingSchool.Data.Data.ConfigExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Data;

public class ApplicationContext : IdentityDbContext<User, IdentityRole<Guid>, Guid,
    IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<UserAppointment> UserAppointments { get; set; }
    public DbSet<Organisation> Organisations { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Appointment>()
            .Property(x => x.Type)
            .HasConversion(x => x.ToString(), x => (AppointmentType)Enum.Parse(typeof(AppointmentType), x));
        
        builder.ConfigureManyToManyRelationships();
        builder.ConfigureQueryFilters();
    }
    
    public override int SaveChanges()
    {
        CheckAndModifyEntryState();
        return base.SaveChanges();
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        CheckAndModifyEntryState();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    
    private void CheckAndModifyEntryState()
    {
        foreach (var entry in ChangeTracker.Entries())
        {

            if (entry.Entity.GetType().GetProperty("UpdatedDate") != null)
                ((BaseModel)entry.Entity).UpdatedDate = DateTime.UtcNow;

            if (entry.State == EntityState.Added && entry.Entity.GetType().GetProperty("CreatedDate") != null)
                ((BaseModel)entry.Entity).CreatedDate = DateTime.UtcNow;

            if (entry.State == EntityState.Deleted && entry.Entity.GetType().GetProperty("IsDeleted") != null)
            {
                if (entry.Metadata.FindProperty("IsDeleted") != null)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                }
            }
        }
    }
}