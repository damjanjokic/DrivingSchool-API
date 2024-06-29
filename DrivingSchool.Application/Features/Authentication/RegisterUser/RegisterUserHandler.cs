using System.Net;
using System.Text;
using AutoMapper;
using DrivingSchool.Application.Errors;
using DrivingSchool.Core.Entities;
using DrivingSchool.Core.IRepositories;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;

namespace DrivingSchool.Application.Features.Authentication.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;

    public RegisterUserHandler(IAuthRepository authRepository, IMapper mapper)
    {
        _authRepository = authRepository;
        _mapper = mapper;
    }
    
    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _authRepository.UserExistsQueryAsync(x => x.NormalizedEmail == request.Email.ToUpper()))
            throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

        var user = _mapper.Map<User>(request);
        var result = await _authRepository.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new RestException(HttpStatusCode.BadRequest, new { Email = "Problem creating user" });

        var token = await _authRepository.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var param = new Dictionary<string, string>
        {
            {"token", token },
            {"email", user.Email }
        };
        //var callback = QueryHelpers.AddQueryString(userRegistrationDto.ClientURI, param);
        /*var emailBody = "Hi " + user.UserName + "<br/>Email Confirmation token<br/>" + "<a href=" + callback + ">Click here</a>";
        _emailService.Send(
            "marque.repo@gmail.com",
            "Marque",
            "Email confirmation",
            emailBody,
            user.Email);*/

        return new RegisterUserResponse()
        {
            Id = user.Id
        };
    }
}