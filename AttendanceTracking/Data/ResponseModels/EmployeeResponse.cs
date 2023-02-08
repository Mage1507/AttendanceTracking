using System;

namespace AttendanceTracking.Data.ResponseModels
{
    public class EmployeeResponse
    {
        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string employeeEmail { get; set; }

        public int managerId { get; set; }
    }
}
