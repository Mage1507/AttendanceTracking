using System;
using System.ComponentModel.DataAnnotations;


namespace AttendanceTracking.Models
{
    public class Attendance
    {

        public int id { get; set; }

        [Required]
        public DateTime date { get; set; }

        public TimeSpan checkInTime { get; set; }

        public TimeSpan? checkOutTime { get; set; }

        public int employeeId { get; set; }
        public Employee employee { get; set; }


    }
}

