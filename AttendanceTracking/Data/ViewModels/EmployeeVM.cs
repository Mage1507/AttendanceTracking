using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracking.Data.ViewModels
{
    public class EmployeeVM
    {
        public string employeeName { get; set; }

        public string employeeEmail { get; set; }

        public string managerEmail { get; set; }
    }
}

