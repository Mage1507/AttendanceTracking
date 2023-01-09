using System;
using AttendanceTracking.Data;
using AttendanceTracking.Models;
namespace AttendanceTracking.Services
{
public class DepartmentService
{
	private readonly DbInitializer _dbContext;
	public DepartmentService(DbInitializer dbContext )
	{
			_dbContext = dbContext;
	}

	public bool AddDepartment(Department department)
		{
			if (department == null)
			{
				return false;
			}
			try
			{
				_dbContext.departments.Add(department);
				_dbContext.SaveChanges();
				return true;
			}catch(Exception ex)
			{
				Console.WriteLine("Exception:" + ex);
				return false;
			}

		}

		public int GetDepartmentId(string departmentName)
		{
			var department = _dbContext.departments.Where(d => d.departmentName == departmentName).FirstOrDefault();
			if (department == null)
			{
				return 0;
			}
			return department.departmentId;
		}
}
}

