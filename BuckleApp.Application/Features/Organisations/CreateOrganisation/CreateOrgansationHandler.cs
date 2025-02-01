using System.Net;
using AutoMapper;
using BuckleApp.Application.Errors;
using BuckleApp.Core.Entities;
using BuckleApp.Core.IRepositories;
using BuckleApp.Core.IUnitOfWork;
using BuckleApp.Infrastructure.Interfaces;
using MediatR;

namespace BuckleApp.Application.Features.Organisations.CreateOrganisation;

public class CreateOrgansationHandler : IRequestHandler<CreateOrganisationCommand, CreateOrganisationResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailService _emailService;
    
    public CreateOrgansationHandler(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _emailService = emailService;
    }
    
    public async Task<CreateOrganisationResponse> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organisation = _mapper.Map<Organisation>(request);
        var organisationExists = await _unitOfWork.Organisation.OrganisationExists(organisation);

        if (organisationExists)
        {
            throw new RestException(HttpStatusCode.BadRequest,
                "Organisation under that name and address already exist");
        }

        await _unitOfWork.Organisation.CreateOrganisation(organisation);
        await _unitOfWork.SaveAsync();

        return new CreateOrganisationResponse()
        {
            Id = organisation.Id
        };
    }
}