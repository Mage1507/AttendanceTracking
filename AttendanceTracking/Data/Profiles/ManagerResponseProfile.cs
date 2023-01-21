using System;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Models;
using AutoMapper;

namespace AttendanceTracking.Data.Profiles
{
    public class ManagerResponseProfile:Profile
    {
       public ManagerResponseProfile(){
            CreateMap<Manager,ManagerResponse> ()
            .ForMember (dest => dest.managerId, opt => opt.MapFrom (src => src.managerId))
            .ForMember (dest => dest.managerName, opt => opt.MapFrom (src => src.managerName))
            .ForMember (dest => dest.managerEmail, opt => opt.MapFrom (src => src.managerEmail))
            .ForMember (dest => dest.departmentId, opt => opt.MapFrom (src => src.departmentId)).ReverseMap();
            
       }
   
    }
}

