using AutoMapper;
using BuckleApp.Application.Features.Authentication.LoginUser;
using BuckleApp.Application.Features.Authentication.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BuckleApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    
    public AuthController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = _mapper.Map<RegisterUserCommand>(request);
        var response = await _mediator.Send(command);

        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var command = _mapper.Map<LoginUserCommand>(request);
        var response = await _mediator.Send(command);

        return Ok(response);
    }
    
}