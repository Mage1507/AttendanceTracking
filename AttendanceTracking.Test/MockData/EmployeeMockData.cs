using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;

namespace AttendanceTracking.Test.MockData;

public static class EmployeeMockData
{
    public static List<EmployeeResponse> GetAllEmployee()
    {
        return new List<EmployeeResponse>
        {
            new EmployeeResponse
            {
              employeeId = 1,
              employeeName = "magesh",
              employeeEmail = "magesh@gmail.com",
              managerId = 1,
            }
        };
    }

    public static IQueryable<Employee> GetEmployeeList()
    {
        return new List<Employee>()
        {
            new Employee()
            {
                employeeId = 1,
                employeeName = "Magesh",
                employeeEmail = "magesh@gmail.com",
                profileImageUrl = "sample.png",
                managerId = 1
            },
            new Employee()
            {
            
                employeeId = 2,
                employeeName = "Kishore",
                employeeEmail = "kishore@gmail.com",
                profileImageUrl = "sample.png",
                managerId = 1
            }
        }.AsQueryable();
    }
}