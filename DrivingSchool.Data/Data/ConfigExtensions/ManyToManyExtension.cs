using DrivingSchool.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DrivingSchool.Data.Data.ConfigExtensions;

public static class ManyToManyExtension
{
    public static void ConfigureManyToManyRelationships(this ModelBuilder builder)
    {
        builder.Entity<UserAppointment>(x =>
        {
            x.HasKey(a => new { a.UserId, a.AppointmentId });

            x.HasOne(a => a.Appointment)
                .WithMany(a => a.UserAppointments)
                .HasForeignKey(a => a.AppointmentId)
                .OnDelete(DeleteBehavior.ClientCascade);

            x.HasOne(a => a.User)
                .WithMany(a => a.UserAppointments)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
        });
    }
}