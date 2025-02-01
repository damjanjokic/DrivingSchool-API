using System.Net;
using System.Text;
using AutoMapper;
using BuckleApp.Application.Errors;
using BuckleApp.Core.Entities;
using BuckleApp.Core.IRepositories;
using BuckleApp.Infrastructure.Email;
using BuckleApp.Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.WebUtilities;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BuckleApp.Application.Features.Authentication.RegisterUser;

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IAuthRepository _authRepository;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;

    public RegisterUserHandler(IAuthRepository authRepository, IMapper mapper, IEmailService emailService)
    {
        _authRepository = authRepository;
        _mapper = mapper;
        _emailService = emailService;
    }
    
    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _authRepository.UserExistsQueryAsync(x => x.NormalizedEmail == request.Email.ToUpper()))
            throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

        var user = _mapper.Map<User>(request);
        var result = await _authRepository.CreateAsync(user, request.Password);

        if (!result.Succeeded)
            throw new RestException(HttpStatusCode.BadRequest, new { Email = "Problem creating user" });

        if (request.Role != null)
        {
           var roleAdded = await _authRepository.AddToRoleAsync(user, request.Role);
           if (!roleAdded)
               throw new RestException(HttpStatusCode.BadRequest, "User role was not saved");
        }

        // var token = await _authRepository.GenerateEmailConfirmationTokenAsync(user);
        // token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        //
        // var param = new Dictionary<string, string>
        // {
        //     {"token", token },
        //     {"email", user.Email }
        // };
        
        // var callback = QueryHelpers.AddQueryString("userRegistrationDto.ClientURI", param);
        // var emailBody = "Hi " + user.UserName + "<br/>Email Confirmation token<br/>" + "<a href=" + callback + ">Click here</a>";
        // var emailResult = await _emailService.Send("Reset Password", emailBody, user.Email);
        //
        // if (!emailResult)
        // {
        //     throw new RestException(HttpStatusCode.BadRequest, "Reset mail is not sent");
        // }
        return new RegisterUserResponse()
        {
            Id = user.Id
        };
    }
}