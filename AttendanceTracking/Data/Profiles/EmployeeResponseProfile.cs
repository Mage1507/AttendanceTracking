using System;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Models;
using AutoMapper;

namespace AttendanceTracking.Data.Profiles
{
    public class EmployeeResponseProfile : Profile
    {
        public EmployeeResponseProfile()
        {
            CreateMap<Employee, EmployeeResponse>()
                .ForMember(dest => dest.employeeId, opt => opt.MapFrom(src => src.employeeId))
                .ForMember(dest => dest.employeeName, opt => opt.MapFrom(src => src.employeeName))
                .ForMember(dest => dest.employeeEmail, opt => opt.MapFrom(src => src.employeeEmail))
                .ForMember(
                    dest => dest.profileImageUrl,
                    opt => opt.MapFrom(src => src.profileImageUrl)
                )
                .ForMember(dest => dest.managerId, opt => opt.MapFrom(src => src.managerId))
                .ReverseMap();
        }
    }
}
