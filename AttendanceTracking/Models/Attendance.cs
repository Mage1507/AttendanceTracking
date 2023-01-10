using System;
using System.ComponentModel.DataAnnotations;


namespace AttendanceTracking.Models
{
    public class Attendance
    {

        public int id { get; set; }


        public DateTime date { get; set; }
        public DateTime checkInTime { get; set; }

        public DateTime? checkOutTime { get; set; }

        public int employeeId { get; set; }
        public Employee employee { get; set; }


    }
}

