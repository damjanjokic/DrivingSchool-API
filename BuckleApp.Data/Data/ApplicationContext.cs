﻿using System.Net.Mime;
using BuckleApp.Core.Entities;
using BuckleApp.Core.Enumerations;
using BuckleApp.Data.Data.ConfigExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuckleApp.Data.Data;

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