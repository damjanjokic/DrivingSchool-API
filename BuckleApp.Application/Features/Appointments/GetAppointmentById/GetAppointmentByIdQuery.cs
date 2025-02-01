using MediatR;

namespace BuckleApp.Application.Features.Appointments.GetAppointmentById;

public class GetByIdQuery : IRequest<GetByIdResponse>
{
    public Guid Id { get; set; }
}