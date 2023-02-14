using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;

namespace AttendanceTracking.Test.MockData;

public static class ManagerMockData
{
    public static List<Manager> GetManagersData()
    {
        return new List<Manager>
        {
            new Manager()
            {
                managerId = 1,
                managerName = "Manager1",
                managerEmail = "manager@gmail.com",
                managerPassword = "manager@123",
            }
        };
    }

    public static List<ManagerVM> ManagerVMList()
    {
        return new List<ManagerVM>
        {
            new ManagerVM()
            {
                managerName = "Magesh",
                managerEmail = "magesh@gmail.com",
                managerPassword = "Magesh*123",
                departmentId = 1
            },
        };
    }

    public static List<ManagerResponse> ManagerResponseList()
    {
        return new List<ManagerResponse>
        {
            new ManagerResponse()
            {
                managerId = 1,
                managerName = "Magesh",
                managerEmail = "magesh@gmail.com",
                departmentId = 1,
            },
        };
    }
    
    public static IQueryable<ManagerResponse> GetAllManagersData()
    {
        return new List<ManagerResponse>
        {
            new ManagerResponse()
            {
                managerId = 1,
                managerName = "Magesh",
                managerEmail = "magesh@gmail.com",
                departmentId = 1,
            }
        }.AsQueryable();
    }
    public static IQueryable<Manager> GetManagersDataQueryable()
    {
        return new List<Manager>
        {
            new Manager()
            {
                managerId = 1,
                managerName = "Manager1",
                managerEmail = "manager@gmail.com",
                managerPassword = "manager@123",
            }
        }.AsQueryable();
    }
}