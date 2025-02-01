using System.Net;
using AutoMapper;
using BuckleApp.Application.Errors;
using BuckleApp.Core.IUnitOfWork;
using BuckleApp.Infrastructure.Interfaces;
using MediatR;

namespace BuckleApp.Application.Features.Appointments.GetAppointmentById;

public class GetByIdHandler : IRequestHandler<GetByIdQuery, GetByIdResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper; 
    private readonly IUserAccessor _userAccessor;

    public GetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    public async Task<GetByIdResponse> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        var appointment = await _unitOfWork.Appointment.GetById(request.Id, true);
        if (appointment == null)
        {
            throw new RestException(HttpStatusCode.NotFound, "There is no such appointment");
        }

        var response = _mapper.Map<GetByIdResponse>(appointment);
        return response;
    }
}