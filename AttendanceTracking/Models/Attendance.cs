using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracking.Models
{
    public class Attendance
    {

        public int id { get; set; }

        [Required]
        public DateTime date { get; set; }

        public DateTime checkInTime { get; set; }

        public DateTime? checkOutTime { get; set; }

        [NotMapped]
        public TimeSpan? totalPresentTime { get; set; }

        [NotMapped]
        public TimeSpan? totalHoursInOffice { get; set; } 
        public int employeeId { get; set; }
        public Employee employee { get; set; }


    }
}

