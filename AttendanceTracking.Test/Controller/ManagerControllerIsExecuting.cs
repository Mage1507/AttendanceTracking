using AttendanceTracking.Controllers;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AttendanceTracking.Test.MockData;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AttendanceTracking.Test.Controller;

public class ManagerControllerIsExecuting
{
    private readonly Mock<ManagerService> _managerService;
    public ManagerControllerIsExecuting()
    {
        _managerService = new Mock<ManagerService>();
    }
    
    
   [Fact]
    public void AddManager()
    {
        //arrange
        var managerList = ManagerMockData.ManagerVMList();

        _managerService.Setup(x => x.AddManager(managerList[0])).Returns(true);
        var managerController = new ManagerController(_managerService.Object);

        //act
        var managerResult = managerController.AddManager(managerList[0]);
        var okManagerResult = managerResult as OkObjectResult;
        
        //assert
        Assert.Equal("Manager Added Successfully", okManagerResult.Value);
    }

    [Fact]
    public void GetAllManager()
    {
        //arrange
        var managerList = ManagerMockData.ManagerResponseList();
        
        _managerService.Setup(x => x.GetAllManagers()).Returns(managerList);
        var managerController = new ManagerController(_managerService.Object);
        
        //act
        var managerResult = managerController.GetAllManagers();
        var okManagerResult = managerResult as OkObjectResult;
        
       //assert
        Assert.Equal(managerList, okManagerResult.Value);
        
    }
    
    [Fact]
    public void GetManagerIdByManagerEmail()
    {
        //arrange
        var managerList = ManagerMockData.GetManagersData();
        
        _managerService.Setup(x => x.GetManagerIdByManagerEmail(managerList[0].managerEmail)).Returns(managerList[0].managerId);
        var managerController = new ManagerController(_managerService.Object);
        
        //act
        var managerResult = managerController.GetManagerIdByManagerEmail(managerList[0].managerEmail);
        var okManagerResult = managerResult as OkObjectResult;
        
        //assert
        Assert.Equal("Manager Id is : 1", okManagerResult.Value);
    }
    
    

    
}