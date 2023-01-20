using System;
using AttendanceTracking.Models;

namespace AttendanceTracking.Data.ViewModels
{
    public class ManagerResponseVM
    {

        public int managerId { get; set; }


        public string managerName { get; set; }

        public string managerEmail { get; set; }


        public int departmentId { get; set; }

        public Department department { get; set; }
    }
}

