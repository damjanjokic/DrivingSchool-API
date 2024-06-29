using AutoMapper;
using DrivingSchool.Application.Dtos;
using DrivingSchool.Application.Features.Appointments.CreateAppointment;
using DrivingSchool.Application.Features.Authentication.LoginUser;
using DrivingSchool.Application.Features.Authentication.RegisterUser;
using DrivingSchool.Core.Entities;

namespace DrivingSchool.Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Auth
        
        //RegisterUser
        CreateMap<RegisterUserRequest, RegisterUserCommand>();
        CreateMap<RegisterUserCommand, User>();
        
        //LoginUser
        CreateMap<LoginUserRequest, LoginUserCommand>();
        
        #endregion
        
        #region Appointment
        
        //CreateAppointment
        CreateMap<CreateAppointmentRequest, CreateAppointmentCommand>();
        CreateMap<CreateAppointmentCommand, Appointment>();
        
        //GetAllByOrganisationId
        CreateMap<Appointment, AppointmentDto>();

        #endregion
    }
}