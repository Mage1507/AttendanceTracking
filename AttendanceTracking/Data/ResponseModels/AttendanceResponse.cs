using System;

namespace AttendanceTracking.Data.ResponseModels
{
    public class AttendanceResponse
    {
        public int id { get; set; }


        public DateTime date { get; set; }


        public DateTime checkInTime { get; set; }


        public DateTime? checkOutTime { get; set; }


        public TimeSpan? totalPresentTime { get; set; }


        public TimeSpan? totalHoursInOffice { get; set; }


        public int employeeId { get; set; }


        public string employeeEmail { get; set; }
    }
}
