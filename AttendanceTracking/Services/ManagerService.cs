using System;
using AttendanceTracking.Data;
using AttendanceTracking.Models;

namespace AttendanceTracking.Services
{
	public class ManagerService
	{
		private readonly DbInitializer _dbContext;

		public DepartmentService _departmentService;
		public ManagerService(DbInitializer dbContext,DepartmentService departmentService)
		{
			_dbContext = dbContext;
			_departmentService = departmentService;
		}
		public bool AddManager(string managerName, string departmentName){
			if (managerName == null||departmentName==null)
			{
				return false;
			}
			try	
			{
				 var departmentId=_departmentService.GetDepartmentId(departmentName);
				_dbContext.managers.Add(new Manager { managerName = managerName, departmentId = departmentId });
				_dbContext.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception:" + ex);
				return false;
			}
		}
	}
}

