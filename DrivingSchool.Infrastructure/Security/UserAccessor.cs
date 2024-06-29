﻿using System.Security.Claims;
using DrivingSchool.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DrivingSchool.Infrastructure.Security;

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentUserId()
    {
        return Guid.Parse(_httpContextAccessor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
    }
}