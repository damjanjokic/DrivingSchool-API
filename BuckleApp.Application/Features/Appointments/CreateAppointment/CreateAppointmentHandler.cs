using System.Net;
using AutoMapper;
using BuckleApp.Application.Errors;
using BuckleApp.Core.Entities;
using BuckleApp.Core.IUnitOfWork;
using BuckleApp.Infrastructure.Interfaces;
using MediatR;

namespace BuckleApp.Application.Features.Appointments.CreateAppointment;

public class CreateAppointmentHandler : IRequestHandler<CreateAppointmentCommand, CreateAppointmentResponse>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public CreateAppointmentHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<CreateAppointmentResponse> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        //var userId = _userAccessor.GetCurrentUserId();
        var appointments = _mapper.Map<List<Appointment>>(request.Appointments);
        foreach (var appointment in appointments)
        {
            appointment.UserId = _userAccessor.GetCurrentUserId();
            
            var overlappingAppointment = await 
                _unitOfWork.Appointment.OverlappingAppointmentAsync(appointment.StartTime, appointment.EndTime,
                    appointment.UserId);
            
            if (overlappingAppointment != null)
            {
                throw new RestException(HttpStatusCode.BadRequest, "Appointment is overlapping");
            }
        }

        await _unitOfWork.Appointment.CreateAppointments(appointments);
        await _unitOfWork.SaveAsync();

        return new CreateAppointmentResponse()
        {
            Ids = appointments.Select(x => x.Id).ToList()
        };
    }
}