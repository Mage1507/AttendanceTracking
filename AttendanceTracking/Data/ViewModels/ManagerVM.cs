﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracking.Data.ViewModels
{
	public class ManagerVM
	{
		public string managerName { get; set; }
		public string managerEmail { get; set; }
		public string departmentName { get; set; }
	}
}
