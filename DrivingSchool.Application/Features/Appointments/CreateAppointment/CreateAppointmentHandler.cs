using System.Net;
using AutoMapper;
using DrivingSchool.Application.Errors;
using DrivingSchool.Core.Entities;
using DrivingSchool.Core.IUnitOfWork;
using DrivingSchool.Infrastructure.Interfaces;
using MediatR;

namespace DrivingSchool.Application.Features.Appointments.CreateAppointment;

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
        var appointment = _mapper.Map<Appointment>(request);
        appointment.UserId = Guid.Parse("BCF63ECB-A507-4618-38EF-08DC961D72B8");

        var isOverlapping = await 
            _unitOfWork.Appointment.IsOverlappingAppointmentAsync(request.StartTime, request.EndTime,
                appointment.UserId);
        
        if (isOverlapping)
        {
            throw new RestException(HttpStatusCode.BadRequest, "Appointment is overlapping");
        }

        await _unitOfWork.Appointment.CreateAppointment(appointment);

        if (request.AttendeeId != null)
        {
            appointment.UserAppointments = new List<UserAppointment>();
            appointment.UserAppointments.Add(new UserAppointment()
            {
                AppointmentId = appointment.Id,
                UserId = request.AttendeeId.Value
            });
            appointment.Set = true;
        }
        
        
        await _unitOfWork.SaveAsync();

        return new CreateAppointmentResponse()
        {
            Id = appointment.Id
        };
    }
}