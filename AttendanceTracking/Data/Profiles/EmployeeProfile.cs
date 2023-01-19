using System;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AutoMapper;

namespace AttendanceTracking.Data.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeVM>()
                .ForMember(dest => dest.employeeName, opt => opt.MapFrom(src => src.employeeName))
                .ForMember(dest => dest.employeeEmail, opt => opt.MapFrom(src => src.employeeEmail))
                .ForMember(dest => dest.managerId, opt => opt.MapFrom(src => src.managerId)).ReverseMap();

        }
    }
}

