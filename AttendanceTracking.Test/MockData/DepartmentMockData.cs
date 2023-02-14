using AttendanceTracking.Models;

namespace AttendanceTracking.Test.MockData;

public static class DepartmentMockData
{
    public static List<Department>  GetDepartmentsData()
    {
        return new List<Department>
        {
            new Department
            {
                departmentId = 1,
                departmentName = "IT"
            },
            new Department
            {
                departmentId = 2,
                departmentName = "HR"
            }
        };
    }
    
    public static IQueryable<Department> GetAllDepartmentsData()
    {
        return new List<Department>
        {
            new Department
            {
                departmentId = 1,
                departmentName = "IT"
            },
            new Department
            {
                departmentId = 2,
                departmentName = "HR"
            }
        }.AsQueryable();
    }
}