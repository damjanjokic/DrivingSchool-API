using AutoMapper;
using BuckleApp.Application.Dtos;
using BuckleApp.Application.Features.Appointments.CreateAppointment;
using BuckleApp.Application.Features.Appointments.GetAppointmentById;
using BuckleApp.Application.Features.Appointments.PatchAppointment;
using BuckleApp.Application.Features.Authentication.LoginUser;
using BuckleApp.Application.Features.Authentication.RegisterUser;
using BuckleApp.Application.Features.Organisations.CreateOrganisation;
using BuckleApp.Core.Entities;

namespace BuckleApp.Application.Mapping;

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
        //CreateMap<CreateAppointmentCommand, Appointment>();
        CreateMap<CreateAppointmentDto, Appointment>();
        
        //EditAppointment
        CreateMap<PatchAppointmentRequest, PatchAppointmentCommand>();
        CreateMap<PatchAppointmentCommand, Appointment>();
        
        //GetAllByOrganisationId
        CreateMap<Appointment, GetAppointmentDto>();
        
        //GetByOrganiserAndDates
        CreateMap<Appointment, GetAppointmentInfoDto>()
            .ForMember(x => x.Date, opt =>
                opt.MapFrom(y => y.StartTime.ToString("dd.MM.yyyy")))
            .ForMember(x => x.Day, opt =>
                opt.MapFrom(y => y.StartTime.Date.ToString("dddd")))
            .ForMember(x => x.Time, opt =>
                opt.MapFrom(a => String.Concat(a.StartTime.ToString("HH:mm"), " - ", a.EndTime.ToString("HH:mm"))))
            .ForMember(x => x.UserCanceled, opt =>
                opt.MapFrom(a =>
                    a.UserAppointments.FirstOrDefault().UserCanceled.FirstName + " " +
                    a.UserAppointments.FirstOrDefault().UserCanceled.LastName))
            .ForMember(x => x.Attendee, opt =>
                opt.MapFrom(y =>
                    y.UserAppointments.FirstOrDefault().User.FirstName + " " +
                    y.UserAppointments.FirstOrDefault().User.FirstName))
            .ForMember(x => x.Type, opt =>
                opt.MapFrom(a => (int)a.Type));

        //GetById
        CreateMap<Appointment, GetByIdResponse>()
            .ForMember(x => x.Date, opt =>
                opt.MapFrom(y => y.StartTime.ToString("dd.MM.yyyy")))
            .ForMember(x => x.StartTime, opt =>
                opt.MapFrom(a => a.StartTime.ToString("HH:mm")))
            .ForMember(x => x.EndTime, opt =>
                opt.MapFrom(a => a.EndTime.ToString("HH:mm")))
            .ForMember(x => x.Attendee, opt =>
                opt.MapFrom(y => 
                    y.UserAppointments.FirstOrDefault().User.FirstName + " " + y.UserAppointments.FirstOrDefault().User.FirstName));

        #endregion

        #region Organisation

        CreateMap<CreateOrganisationRequest, CreateOrganisationCommand>();
        CreateMap<CreateOrganisationCommand, Organisation>();

        #endregion
    }
}