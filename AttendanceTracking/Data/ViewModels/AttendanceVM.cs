using System;
namespace AttendanceTracking.Data.ViewModels
{
	public class AttendanceVM
	{
		public string managerEmail { get; set; }
		public DateTime date { get; set; }

		public DateTime fromTime{ get; set; }

		public DateTime toTime { get; set; }
	}
}

