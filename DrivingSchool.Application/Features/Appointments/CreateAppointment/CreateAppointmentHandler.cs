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
        var appointments = _mapper.Map<List<Appointment>>(request.Appointments);
        foreach (var appointment in appointments)
        {
            
            appointment.UserId = Guid.Parse("38db990d-d53a-4795-3fc5-08dc9848a982");
            
            var isOverlapping = await 
                _unitOfWork.Appointment.IsOverlappingAppointmentAsync(appointment.StartTime, appointment.EndTime,
                    appointment.UserId);
            
            if (isOverlapping)
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