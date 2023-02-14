using AttendanceTracking.Data;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AttendanceTracking.Test.MockData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AttendanceTracking.Test.Services;

public class DepartmentServiceIsExecuting
{
    private readonly Mock<ILogger<DepartmentService>> _logger;
    private readonly Mock<DbSet<Department>> _mock;
    private readonly Mock<DbInitializer> _mockContext;
    
    public DepartmentServiceIsExecuting()
    {
        _logger = new Mock<ILogger<DepartmentService>>();
        _mock = new Mock<DbSet<Department>>();
        _mockContext = new Mock<DbInitializer>();
    }
    
    [Fact]
    public void GetAllDepartments()
    {
        //arrange
        var departmentList = DepartmentMockData.GetAllDepartmentsData();
        
        _mock.As<IQueryable<Department>>().Setup(m => m.Provider).Returns(departmentList.Provider);
        _mock.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(departmentList.Expression);
        _mock.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(departmentList.ElementType);
        _mock.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(() => departmentList.GetEnumerator());
        
        _mockContext.Setup(x => x.departments).Returns(_mock.Object);
        var departmentService = new DepartmentService(_mockContext.Object, _logger.Object);
        
        //act
        var result = departmentService.GetAllDepartments();
        
        //assert
        Assert.Equal(departmentList, result);
    }
    
     [Fact]
     public void GetDepartmentIdByName()
     {
         //arrange
         var departmentList = DepartmentMockData.GetAllDepartmentsData();
         
         _mock.As<IQueryable<Department>>().Setup(m => m.Provider).Returns(departmentList.Provider);
         _mock.As<IQueryable<Department>>().Setup(m => m.Expression).Returns(departmentList.Expression);
         _mock.As<IQueryable<Department>>().Setup(m => m.ElementType).Returns(departmentList.ElementType);
         _mock.As<IQueryable<Department>>().Setup(m => m.GetEnumerator()).Returns(() => departmentList.GetEnumerator());
        
         _mockContext.Setup(x => x.departments).Returns(_mock.Object);
         
         var departmentService = new DepartmentService(_mockContext.Object, _logger.Object);
         
         //act
         var result = departmentService.GetDepartmentIdByName("IT");
         
         //assert
         Assert.Equal(1, result);
     }

    
     
  
}