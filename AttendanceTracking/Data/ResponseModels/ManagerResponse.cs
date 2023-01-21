using System;
using AttendanceTracking.Models;

namespace AttendanceTracking.Data.ResponseModels
{
    public class ManagerResponse
    {
        public int managerId { get; set; }

        public string managerName { get; set; }

        public string managerEmail { get; set; }

        public int departmentId { get; set; }
    }
}
