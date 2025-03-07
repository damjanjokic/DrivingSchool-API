﻿using System.Net;

namespace DrivingSchool.Application.Errors;

public class RestException : Exception
{
    public RestException()
    {
    }

    public RestException(HttpStatusCode code, object errors = null)
    {
        Code = code;
        Errors = errors;
    }

    public HttpStatusCode Code { get; }
    public object Errors { get; }
}