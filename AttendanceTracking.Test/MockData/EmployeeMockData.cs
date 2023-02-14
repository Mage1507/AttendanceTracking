using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;

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
}