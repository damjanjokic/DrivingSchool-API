using AutoMapper;
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
    
    public Task<GetByOrganiserAndDateResponse> Handle(GetByOrganiserAndDateQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}