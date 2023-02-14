using AttendanceTracking.Data;
using AttendanceTracking.Data.Profiles;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AttendanceTracking.Test.MockData;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace AttendanceTracking.Test.Services;

public class ManagerServiceIsExecuting
{
    private readonly Mock<ILogger<ManagerService>> _logger;
    private readonly Mock<DbSet<Manager>> _mock;
    private readonly Mock<DbInitializer> _mockContext;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IConfiguration> _mockConfig;
    private readonly Mock<DepartmentService> _mockDepartmentService;

    public ManagerServiceIsExecuting()
    {
        _logger = new Mock<ILogger<ManagerService>>();
        _mock = new Mock<DbSet<Manager>>();
        _mockContext = new Mock<DbInitializer>();
        _mapper = new Mock<IMapper>();
        _mockConfig = new Mock<IConfiguration>();
        _mockDepartmentService = new Mock<DepartmentService>();
    }

    [Fact]
    public void GetManagerIdByManagerEmail()
    {
        //arrange
        var managerList = ManagerMockData.GetManagersDataQueryable();

        _mock.As<IQueryable<Manager>>().Setup(m => m.Provider).Returns(managerList.Provider);
        _mock.As<IQueryable<Manager>>().Setup(m => m.Expression).Returns(managerList.Expression);
        _mock.As<IQueryable<Manager>>().Setup(m => m.ElementType).Returns(managerList.ElementType);
        _mock.As<IQueryable<Manager>>().Setup(m => m.GetEnumerator()).Returns(() => managerList.GetEnumerator());

        _mockContext.Setup(x => x.managers).Returns(_mock.Object);

        var managerService = new ManagerService(_mockContext.Object, _mockDepartmentService.Object, _logger.Object,
            _mapper.Object, _mockConfig.Object);
        //act
        var result = managerService.GetManagerIdByManagerEmail("manager@gmail.com");

        //assert
        Assert.Equal(1, result);

     }

    [Fact]
    public void GetManagerEmail()
    {
        //arrange
        var managerList = ManagerMockData.GetManagersDataQueryable();

        _mock.As<IQueryable<Manager>>().Setup(m => m.Provider).Returns(managerList.Provider);
        _mock.As<IQueryable<Manager>>().Setup(m => m.Expression).Returns(managerList.Expression);
        _mock.As<IQueryable<Manager>>().Setup(m => m.ElementType).Returns(managerList.ElementType);
        _mock.As<IQueryable<Manager>>().Setup(m => m.GetEnumerator()).Returns(() => managerList.GetEnumerator());

        _mockContext.Setup(x => x.managers).Returns(_mock.Object);

        var managerService = new ManagerService(_mockContext.Object, _mockDepartmentService.Object, _logger.Object,
            _mapper.Object, _mockConfig.Object);
        //act
        var result = managerService.GetManagerEmail(1);

        //assert
        Assert.Equal("manager@gmail.com", result);
    }
    
   
        














}