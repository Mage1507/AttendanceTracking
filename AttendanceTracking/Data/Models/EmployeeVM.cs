using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracking.Data.Models
{
	public class EmployeeVM
	{
	   public string employeeName { get; set; }

       [EmailAddress]
	   public string employeeEmail { get; set; }
	   
       [EmailAddress]
	   public string managerEmail { get; set; }
	}
}

