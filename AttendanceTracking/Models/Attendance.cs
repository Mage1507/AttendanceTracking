using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracking.Models
{
	public class Attendance
	{
		public int id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime date { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss tt}")]
        public DateTime checkInTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm:ss tt}")]
        public DateTime checkOutTime { get; set; }

        public int employeeId { get; set; }
        public Employee employee { get; set; }
	}
}

