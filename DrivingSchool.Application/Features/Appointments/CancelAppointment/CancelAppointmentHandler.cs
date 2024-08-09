using System.Net;
using AutoMapper;
using DrivingSchool.Application.Errors;
using DrivingSchool.Core.IRepositories;
using DrivingSchool.Core.IUnitOfWork;
using DrivingSchool.Infrastructure.Interfaces;
using MediatR;

namespace DrivingSchool.Application.Features.Appointments.CancelAppointment;

public class CancelAppointmentHandler : IRequestHandler<CancelAppointmentCommand, CancelAppointmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public CancelAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }
    
    public async Task<CancelAppointmentResponse> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Appointment.GetById(request.Id, true);
        if (appointment == null)
        {
            throw new RestException(HttpStatusCode.NotFound, "Appointment not found");
        }

        var userAppointment =  appointment.UserAppointments.First();
        if (userAppointment == null)
        {
            throw new RestException(HttpStatusCode.NotFound, "Your are not part of this appointment");
        }

        userAppointment.isCanceled = true;
        userAppointment.UserCanceledId = Guid.Parse("38db990d-d53a-4795-3fc5-08dc9848a982");
        userAppointment.CancelationReason = request.Reason;

        return new CancelAppointmentResponse()
        {
            Id = Guid.Parse("38db990d-d53a-4795-3fc5-08dc9848a982")
        };
    }
}