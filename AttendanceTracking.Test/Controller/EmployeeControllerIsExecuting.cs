using AttendanceTracking.Controllers;
using AttendanceTracking.Services;
using AttendanceTracking.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AttendanceTracking.Test.Controller;

public class EmployeeControllerIsExecuting
{
    private readonly  Mock<EmployeeService> _employeeService;
    
    public EmployeeControllerIsExecuting()
    {
        _employeeService = new Mock<EmployeeService>();
    }

    [Fact]
    public void GetAllEmployees()
    {
        //arrange
        var employeeList = EmployeeMockData.GetAllEmployee();
        _employeeService.Setup(x => x.GetAllEmployees()).Returns(employeeList);
        var employeeController = new EmployeeController(_employeeService.Object);
        
        //act
        var employeeResult = employeeController.GetAllEmployees();
        var okEmployeeResult = employeeResult as OkObjectResult;
        
        //assert
        Assert.Equal(employeeList, okEmployeeResult.Value);

    }


}