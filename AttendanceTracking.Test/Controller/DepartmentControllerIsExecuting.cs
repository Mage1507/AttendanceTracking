using AttendanceTracking.Controllers;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AttendanceTracking.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AttendanceTracking.Test.Controller;

public class DepartmentControllerIsExecuting
{
    private readonly Mock<DepartmentService> _departmentService;

    public DepartmentControllerIsExecuting()
    {
        _departmentService = new Mock<DepartmentService>();
    }

    [Fact]
    public void AddDepartment()
    {
        //arrange
        var departmentList = DepartmentMockData.GetDepartmentsData();

        _departmentService.Setup(x => x.AddDepartment(departmentList[0])).Returns(true);
        var departmentController = new DepartmentController(_departmentService.Object);

        //act
        var departmentResult = departmentController.AddDepartment(departmentList[0]);
        var okDepartmentResult = departmentResult as OkObjectResult;
        
        //assert
        Assert.Equal("Department Added Successfully", okDepartmentResult.Value);
    }
    
    [Fact]
    public void GetAllDepartments()
    {
        //arrange
        var departmentList = DepartmentMockData.GetDepartmentsData();

        _departmentService.Setup(x => x.GetAllDepartments()).Returns(departmentList);
        var departmentController = new DepartmentController(_departmentService.Object);

        //act
        var departmentResult = departmentController.GetAllDepartments();
        var okDepartmentResult = departmentResult as OkObjectResult;
        
        //assert
        Assert.Equal(departmentList, okDepartmentResult.Value);
    }
    
    [Fact]
    public void GetDepartmentIdByName()
    {
        //arrange
        var departmentList = DepartmentMockData.GetDepartmentsData();

        _departmentService.Setup(x => x.GetDepartmentIdByName(departmentList[0].departmentName)).Returns(departmentList[0].departmentId);
        var departmentController = new DepartmentController(_departmentService.Object);

        //act
        var departmentResult = departmentController.GetDepartmentIdByName(departmentList[0].departmentName);
        var okDepartmentResult = departmentResult as OkObjectResult;
        
        //assert
        Assert.Equal("Department Id is : "+departmentList[0].departmentId, okDepartmentResult.Value);
    }
    

}