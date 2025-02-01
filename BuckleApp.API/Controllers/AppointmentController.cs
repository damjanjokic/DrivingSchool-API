using AutoMapper;
using BuckleApp.Application.Features.Appointments.CancelAppointment;
using BuckleApp.Application.Features.Appointments.CreateAppointment;
using BuckleApp.Application.Features.Appointments.DeleteAppointment;
using BuckleApp.Application.Features.Appointments.GetAppointmentById;
using BuckleApp.Application.Features.Appointments.GetAppointmentsByOrganiserAndDate;
using BuckleApp.Application.Features.Appointments.PatchAppointment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuckleApp.API.Controllers;

// [Authorize]
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

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointmentById(Guid id)
    {
        var query = new GetByIdQuery()
        {
            Id = id
        };

        var response = await _mediator.Send(query);
        return Ok(response);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = _mapper.Map<CreateAppointmentCommand>(request);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAppointment(Guid id, [FromBody] PatchAppointmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = _mapper.Map<PatchAppointmentCommand>(request);
        command.Id = id;
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

    [HttpPatch("cancelAppointment/{id}")]
    public async Task<IActionResult> CancelAppointment(Guid id, [FromBody] CancelAppointmentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest();

        var command = new CancelAppointmentCommand()
        {
            Id = id,
            Reason = request.Reason
        };

        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(Guid id)
    {
        var command = new DeleteAppointmentCommand()
        {
            Id = id
        };

        var response = await _mediator.Send(command);

        return Ok(response);
    }
}