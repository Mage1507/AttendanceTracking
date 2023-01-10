using System;
using AttendanceTracking.Data;
using AttendanceTracking.Data.Models;
using AttendanceTracking.Models;

namespace AttendanceTracking.Services
{
	public class EmployeeService
	{
		private DbInitializer _dbContext;

		private ManagerService _managerService;
		public EmployeeService(DbInitializer dbContext, ManagerService managerService)
		{
			_dbContext = dbContext;
			_managerService = managerService;
		}
		public bool AddEmployee(EmployeeVM employeeVM)
		{
			if (employeeVM == null)
			{
				return false;
			}
			try
			{
				var managerId = _managerService.GetManagerId(employeeVM.managerEmail);
				Employee employee = new Employee()
				{
					employeeName = employeeVM.employeeName,
					employeeEmail = employeeVM.employeeEmail,
					managerId = managerId
				};
				_dbContext.employees.Add(employee);
				_dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception:" + ex);
				return false;
			}
		}

		public int GetEmployeeId(string employeeEmail){
			var employee = _dbContext.employees.Where(e => e.employeeEmail == employeeEmail).FirstOrDefault();
			return employee.employeeId;
		}
	}
}

