using System.Net;
using AutoMapper;
using BuckleApp.Application.Errors;
using BuckleApp.Core.Entities;
using BuckleApp.Core.IUnitOfWork;
using BuckleApp.Infrastructure.Interfaces;
using MediatR;

namespace BuckleApp.Application.Features.Appointments.PatchAppointment;

public class PatchAppointmentHandler : IRequestHandler<PatchAppointmentCommand, PatchAppointmentResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 
    private readonly IUserAccessor _userAccessor;
    
    public PatchAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }
    public async Task<PatchAppointmentResponse> Handle(PatchAppointmentCommand request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Appointment.GetById(request.Id);

        if (appointment == null)
        {
            throw new RestException(HttpStatusCode.NotFound, "There is no such appointment");
        }

        if (appointment.StartTime < DateTime.UtcNow)
        {
            throw new RestException(HttpStatusCode.BadRequest, "You cannot edit appointment that already started");
        }

        appointment.Note = request.Note;
        appointment.Type = request.Type;
        
        await _unitOfWork.SaveAsync();

        return new PatchAppointmentResponse()
        {
            Id = appointment.Id
        };
    }
}