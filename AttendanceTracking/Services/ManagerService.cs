using System;
using AttendanceTracking.Data;
using AttendanceTracking.Models;
using AttendanceTracking.Data.Models;

namespace AttendanceTracking.Services
{
    public class ManagerService
    {
        private readonly DbInitializer _dbContext;

        public DepartmentService _departmentService;
        public ManagerService(DbInitializer dbContext, DepartmentService departmentService)
        {
            _dbContext = dbContext;
            _departmentService = departmentService;
        }
        public bool AddManager(ManagerVM managerVM)
        {
            if (managerVM == null)
            {
                return false;
            }
            try
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex);
                return false;
            }
        }

        public int GetManagerId(string managerEmail)
        {
            var manager = _dbContext.managers.Where(m => m.managerEmail == managerEmail).FirstOrDefault();
            return manager.managerId;
        }
    }
}

