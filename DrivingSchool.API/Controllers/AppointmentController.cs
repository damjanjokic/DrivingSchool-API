using AutoMapper;
using DrivingSchool.Application.Features.Appointments.CreateAppointment;
using DrivingSchool.Application.Features.Appointments.GetAllByOrganiserId;
using DrivingSchool.Application.Features.Appointments.GetByOrganiserAndDate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DrivingSchool.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AppointmentController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = _mapper.Map<CreateAppointmentCommand>(request);
        var response = await _mediator.Send(command);

        return Ok(response);
    }
    
    [HttpGet("organiser/{organiserId}")]
    public async Task<IActionResult> GetAppointmentsByOrganiserIdAndDates(Guid organiserId, [FromQuery] List<DateTime> dates)
    {
        var query = new GetByOrganiserAndDateQuery()
        {
            OrganiserId = organiserId,
            Dates = dates
        };

        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
}