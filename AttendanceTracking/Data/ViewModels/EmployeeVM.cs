using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AttendanceTracking.Data.ViewModels
{
    public class EmployeeVM
    {
        public string employeeName { get; set; }

        public string employeeEmail { get; set; }
        
        public IFormFile profileImageUrl { get; set; }
        
        public int managerId { get; set; }
    }
}
