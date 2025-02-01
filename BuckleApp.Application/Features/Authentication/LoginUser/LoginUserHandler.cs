using System.Net;
using AutoMapper;
using BuckleApp.Application.Errors;
using BuckleApp.Core.IRepositories;
using BuckleApp.Infrastructure.Interfaces;
using MediatR;

namespace BuckleApp.Application.Features.Authentication.LoginUser;

public class LoginUserHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    private readonly IJwtGenerator _jwtGenerator;
    
    public LoginUserHandler(IAuthRepository authRepository, IMapper mapper, IJwtGenerator jwtGenerator)
    {
        _authRepository = authRepository;
        _mapper = mapper;
        _jwtGenerator = jwtGenerator;
    }
    
    public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _authRepository.FindByEmailAsync(request.Email);

        if (user == null)
            throw new RestException(HttpStatusCode.Unauthorized);

        /*if (!user.EmailConfirmed)
            throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email is not confirmed" });*/

        var result = await _authRepository
            .CheckPasswordSignInAsync(user, request.Password, false);
        var roles = await _authRepository.GetUserRoles(user);
        if (result.Succeeded)
        {
            return new LoginUserResponse()
            {
                Token = _jwtGenerator.CreateToken(user, roles.ToList())
            };
        }

        throw new RestException(HttpStatusCode.Unauthorized);
    }
}