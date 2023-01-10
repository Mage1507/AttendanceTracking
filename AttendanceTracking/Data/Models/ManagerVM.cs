﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracking.Data.Models
{
	public class ManagerVM
	{
		public string managerName { get; set; }
		[EmailAddress]
		public string managerEmail { get; set; }
		public string departmentName { get; set; }
	}
}

