using System;
using AttendanceTracking.Data;
using AttendanceTracking.Models;
using AttendanceTracking.Data.ViewModels;

namespace AttendanceTracking.Services
{
    public class ManagerService
    {
        private readonly DbInitializer _dbContext;

        public DepartmentService _departmentService;

        private readonly ILogger<ManagerService> _logger;
        public ManagerService(DbInitializer dbContext, DepartmentService departmentService, ILogger<ManagerService> logger)
        {
            _dbContext = dbContext;
            _departmentService = departmentService;
            _logger = logger;
        }
        public bool AddManager(ManagerVM managerVM)
        {
            if (managerVM == null)
            {
                return false;
            }
            try
            {
                if (IsManagerEmailExist(managerVM.managerEmail))
                {
                    return false;
                }
                else
                {
                    var departmentId = _departmentService.GetDepartmentId(managerVM.departmentName);
                    Manager manager = new Manager()
                    {
                        managerName = managerVM.managerName,
                        managerEmail = managerVM.managerEmail,
                        departmentId = departmentId
                    };
                    _dbContext.managers.Add(manager);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in Add Manager Method : " + ex.Message);
                return false;
            }
        }

        public int GetManagerId(string managerEmail)
        {
            var manager = _dbContext.managers.Where(m => m.managerEmail == managerEmail).FirstOrDefault();
            return manager.managerId;
        }

        public bool IsManagerEmailExist(string managerEmail)
        {
            var manager = _dbContext.managers.Where(m => m.managerEmail == managerEmail).FirstOrDefault();
            if (manager != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

