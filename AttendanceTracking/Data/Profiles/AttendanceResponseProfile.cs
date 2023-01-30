using System;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Models;
using AutoMapper;

namespace AttendanceTracking.Data.Profiles
{
    public class AttendanceResponseProfile : Profile
    {
        public AttendanceResponseProfile()
        {
            CreateMap<Attendance, AttendanceResponse>()
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id))
                .ForMember(dest => dest.date, opt => opt.MapFrom(src => src.date))
                .ForMember(dest => dest.checkInTime, opt => opt.MapFrom(src => src.checkInTime))
                .ForMember(dest => dest.checkOutTime, opt => opt.MapFrom(src => src.checkOutTime))
                .ForMember(
                    dest => dest.totalPresentTime,
                    opt => opt.MapFrom(src => src.totalPresentTime)
                )
                .ForMember(
                    dest => dest.totalHoursInOffice,
                    opt => opt.MapFrom(src => src.totalHoursInOffice)
                )
                .ForMember(dest => dest.employeeId, opt => opt.MapFrom(src => src.employeeId))
                .ReverseMap();
        }
    }
}
