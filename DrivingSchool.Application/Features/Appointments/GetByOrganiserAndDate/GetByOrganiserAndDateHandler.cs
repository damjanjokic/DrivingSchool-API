using AutoMapper;
using DrivingSchool.Application.Dtos;
using DrivingSchool.Application.Extensions;
using DrivingSchool.Core.IUnitOfWork;
using DrivingSchool.Infrastructure.Interfaces;
using MediatR;

namespace DrivingSchool.Application.Features.Appointments.GetByOrganiserAndDate;

public class GetByOrganiserAndDateHandler : IRequestHandler<GetByOrganiserAndDateQuery, GetByOrganiserAndDateResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;
    
    public GetByOrganiserAndDateHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }
    
    public async Task<GetByOrganiserAndDateResponse> Handle(GetByOrganiserAndDateQuery request, CancellationToken cancellationToken)
    {
        var onlyDates = request.Dates.Select(x => x.Date.Date).ToList();
        var appointments = await _unitOfWork.Appointment.GetAllByOrganiserIdAndDates(request.OrganiserId, onlyDates);
        foreach (var appointment in appointments)
        {
            appointment.StartTime = appointment.StartTime.ConvertToTimeZone("Central Europe Standard Time");
            appointment.EndTime = appointment.EndTime.ConvertToTimeZone("Central Europe Standard Time");
        }
        var appointmentsDto = _mapper.Map<List<GetAppointmentInfoDto>>(appointments);
        return new GetByOrganiserAndDateResponse()
        {
            Appointments = appointmentsDto
        };
        
    }
}