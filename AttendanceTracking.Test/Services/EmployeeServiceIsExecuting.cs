using AttendanceTracking.Data;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AttendanceTracking.Test.MockData;
using AutoMapper;
using AwsS3.Services;
using AwsSecretManager.Interface;
using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace AttendanceTracking.Test.Services;

public class EmployeeServiceIsExecuting
{
    private readonly Mock<ILogger<EmployeeService>> _logger;
    private readonly Mock<DbSet<Employee>> _mock;
    private readonly Mock<DbInitializer> _mockContext;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IConfigSettings> _mockConfig;
    private readonly Mock<ManagerService> _mockManagerService;
    private readonly Mock<IStorageService> _mockStorageService;

    public EmployeeServiceIsExecuting()
    {
        _logger = new Mock<ILogger<EmployeeService>>();
        _mock = new Mock<DbSet<Employee>>();
        _mockContext = new Mock<DbInitializer>();
        _mapper = new Mock<IMapper>();
        _mockConfig = new Mock<IConfigSettings>();
        _mockManagerService = new Mock<ManagerService>();
        _mockStorageService = new Mock<IStorageService>();
    }

    [Fact]
    public void GetEmployeeListByManagerId()
    {
        var employeeList = EmployeeMockData.GetEmployeeList();
        _mock.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employeeList.Provider);
        _mock.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employeeList.Expression);
        _mock.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employeeList.ElementType);
        _mock.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(() => employeeList.GetEnumerator());

        _mockContext.Setup(x => x.employees).Returns(_mock.Object);
        
        var employeeService = new EmployeeService(_mockContext.Object,_mockManagerService.Object,_logger.Object,_mapper.Object,_mockStorageService.Object,_mockConfig.Object);
        var result = employeeService.GetEmployeeListByManagerId(1);
        Assert.Equal(2, result.Count);
        
    }

    [Fact]
    public void GetEmployeeIdByEmployeeEmail()
    {
        var employeeList = EmployeeMockData.GetEmployeeList();
        _mock.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employeeList.Provider);
        _mock.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employeeList.Expression);
        _mock.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employeeList.ElementType);
        _mock.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(() => employeeList.GetEnumerator());

        _mockContext.Setup(x => x.employees).Returns(_mock.Object);
        
        var employeeService = new EmployeeService(_mockContext.Object,_mockManagerService.Object,_logger.Object,_mapper.Object,_mockStorageService.Object,_mockConfig.Object);
        var result = employeeService.GetEmployeeIdByEmployeeEmail("magesh@gmail.com");
        Assert.Equal(1, result);
    }

    [Fact]
    public void GetEmployeeEmailById()
    {
        var employeeList = EmployeeMockData.GetEmployeeList();
        _mock.As<IQueryable<Employee>>().Setup(m => m.Provider).Returns(employeeList.Provider);
        _mock.As<IQueryable<Employee>>().Setup(m => m.Expression).Returns(employeeList.Expression);
        _mock.As<IQueryable<Employee>>().Setup(m => m.ElementType).Returns(employeeList.ElementType);
        _mock.As<IQueryable<Employee>>().Setup(m => m.GetEnumerator()).Returns(() => employeeList.GetEnumerator());

        _mockContext.Setup(x => x.employees).Returns(_mock.Object);
        
        var employeeService = new EmployeeService(_mockContext.Object,_mockManagerService.Object,_logger.Object,_mapper.Object,_mockStorageService.Object,_mockConfig.Object);
        var result = employeeService.GetEmployeeEmailById(1);
        Assert.Equal("magesh@gmail.com", result);
    }
    
    
    
}

