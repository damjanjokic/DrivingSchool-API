using System.Net;
using AutoMapper;
using BuckleApp.Application.Errors;
using BuckleApp.Core.IUnitOfWork;
using BuckleApp.Infrastructure.Interfaces;
using MediatR;

namespace BuckleApp.Application.Features.Appointments.DeleteAppointment;

public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, DeleteAppointmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;
    
    public DeleteAppointmentHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserAccessor userAccessor)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userAccessor = userAccessor;
    }

    public async Task<DeleteAppointmentResponse> Handle(DeleteAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Appointment.GetById(request.Id, true);

        if (appointment == null)
        {
            throw new RestException(HttpStatusCode.NotFound, "Appointment not found");
        }

        if (appointment.UserAppointments.Any())
        {
            throw new RestException(HttpStatusCode.BadRequest, "Appointment with attendee cannot be deleted");
        }
        
        _unitOfWork.Appointment.Remove(appointment);
        await _unitOfWork.SaveAsync();

        return new DeleteAppointmentResponse()
        {
            Id = request.Id
        };
    }
}