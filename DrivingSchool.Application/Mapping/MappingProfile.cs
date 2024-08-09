using AutoMapper;
using DrivingSchool.Application.Dtos;
using DrivingSchool.Application.Features.Appointments.CreateAppointment;
using DrivingSchool.Application.Features.Appointments.GetByOrganiserAndDate;
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
        CreateMap<CreateAppointmentDto, Appointment>();
        
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
                    a.UserAppointments.FirstOrDefault().UserCanceled.FirstName + " " + a.UserAppointments.FirstOrDefault().UserCanceled.LastName))
            .ForMember(x => x.Attendee, opt =>
                opt.MapFrom(y => 
                    y.UserAppointments.FirstOrDefault().User.FirstName + " " + y.UserAppointments.FirstOrDefault().User.FirstName));
        
        
        #endregion
    }
}