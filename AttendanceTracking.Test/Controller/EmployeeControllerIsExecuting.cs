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

    [Fact]
    public void GetEmployeeIdByEmployeeEmail()
    {
        //arrange
        var employeeList = EmployeeMockData.GetAllEmployee();
        _employeeService.Setup(x => x.GetEmployeeIdByEmployeeEmail(employeeList[0].employeeEmail)).Returns(employeeList[0].employeeId);
        var employeeController = new EmployeeController(_employeeService.Object);
        
        //act
        var employeeResult = employeeController.GetEmployeeIdByEmployeeEmail(employeeList[0].employeeEmail);
        var okEmployeeResult = employeeResult as OkObjectResult;
        
        //assert
        Assert.Equal("Employee Id is : "+employeeList[0].employeeId, okEmployeeResult.Value);
    }


}