﻿using BuckleApp.Core.IRepositories;

namespace BuckleApp.Core.IUnitOfWork;

public interface IUnitOfWork
{
    IAppointmentRepository Appointment { get; }
    IOrganisationRepository Organisation { get; }
    Task SaveAsync();
}