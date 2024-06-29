using System.Net;
using AutoMapper;
using DrivingSchool.Application.Errors;
using DrivingSchool.Core.IRepositories;
using DrivingSchool.Infrastructure.Interfaces;
using MediatR;

namespace DrivingSchool.Application.Features.Authentication.LoginUser;

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

        if (result.Succeeded)
        {
            return new LoginUserResponse()
            {
                Email = user.Email,
                Token = _jwtGenerator.CreateToken(user)
            };
        }

        throw new RestException(HttpStatusCode.Unauthorized);
    }
}