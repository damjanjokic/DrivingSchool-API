using AutoMapper;
using DrivingSchool.Application.Dtos;
using DrivingSchool.Core.IUnitOfWork;
using DrivingSchool.Infrastructure.Interfaces;
using MediatR;

namespace DrivingSchool.Application.Features.Appointments.GetAllByOrganiserId;

public class GetAllByOrganiserIdHandler : IRequestHandler<GetAllByOrganiserIdQuery, GetAllByOrganiserIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public GetAllByOrganiserIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }
    
    public async Task<GetAllByOrganiserIdResponse> Handle(GetAllByOrganiserIdQuery request, CancellationToken cancellationToken)
    {
        var appointments = await _unitOfWork.Appointment.GetAllByOrganiserId(request.Id);
        var response = new GetAllByOrganiserIdResponse()
        {
            Appointments = _mapper.Map<List<AppointmentDto>>(appointments)
        };

        return response;
    }
}