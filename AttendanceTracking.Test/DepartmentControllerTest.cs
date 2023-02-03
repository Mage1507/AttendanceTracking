using AttendanceTracking.Controllers;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AttendanceTracking.Test;

public class DepartmentControllerTest
{
    private readonly Mock<DepartmentService> departmentService;
    public DepartmentControllerTest()
    {
        departmentService = new Mock<DepartmentService>();
    }
    [Fact]
    public void GetAllDepartmentsTest()
    {
      //Arrange
      var departmentList = GetAllDepartments();
      departmentService.Setup(x => x.GetAllDepartments()).Returns(departmentList);
      var departmentController = new DepartmentController(departmentService.Object);

      //Act
      var departmentResult = departmentController.GetAllDepartments();

      //Assert
        Assert.NotNull(departmentResult);
        Assert.IsType<OkObjectResult>(departmentResult);
        var okResult = departmentResult as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<List<Department>>(okResult.Value);
        var department = okResult.Value as List<Department>;
        Assert.NotNull(department);
        Assert.Equal(1, department.Count);
    }
    
    [Fact]
    public void AddDepartmentTest()
    {
      //Arrange
      var departmentList = GetAllDepartments();
      departmentService.Setup(x => x.AddDepartment(departmentList[0])).Returns(true);
      var productController = new DepartmentController(departmentService.Object);

      //Act
        var departmentResult = productController.AddDepartment(departmentList[0]);
        
      //Assert
        Assert.NotNull(departmentResult);
        Assert.IsType<OkObjectResult>(departmentResult);
        var okResult = departmentResult as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(200, okResult.StatusCode);
        Assert.IsType<string>(okResult.Value);
        var department = okResult.Value;
        Assert.NotNull(department);
    }
    
    
    private List<Department> GetAllDepartments()
    {
       var department = new List<Department>();
       department.Add(new Department { departmentId = 1, departmentName = "IT" });
       return department;
    }
}