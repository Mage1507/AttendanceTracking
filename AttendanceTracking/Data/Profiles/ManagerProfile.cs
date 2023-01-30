using System;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AutoMapper;

namespace AttendanceTracking.Data.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<Manager, ManagerVM>()
                .ForMember(dest => dest.managerName, opt => opt.MapFrom(src => src.managerName))
                .ForMember(dest => dest.managerEmail, opt => opt.MapFrom(src => src.managerEmail))
                .ForMember(
                    dest => dest.managerPassword,
                    opt => opt.MapFrom(src => src.managerPassword)
                )
                .ForMember(dest => dest.departmentId, opt => opt.MapFrom(src => src.departmentId))
                .ReverseMap();
        }
    }
}
