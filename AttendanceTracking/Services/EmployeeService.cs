using System;
using AttendanceTracking.Data;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;

namespace AttendanceTracking.Services
{
    public class EmployeeService
    {
        private DbInitializer _dbContext;

        private ManagerService _managerService;

        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(DbInitializer dbContext, ManagerService managerService, ILogger<EmployeeService> logger)
        {
            _dbContext = dbContext;
            _managerService = managerService;
            _logger = logger;
        }

        public bool AddEmployee(EmployeeVM employeeVM)
        {
            if (employeeVM == null)
            {
                return false;
            }
            try
            {
                if (IsEmployeeEmailExist(employeeVM.employeeEmail))
                {
                    return false;
                }
                else
                {
                    var managerId = _managerService.GetManagerId(employeeVM.managerEmail);
                    Employee employee = new Employee()
                    {
                        employeeName = employeeVM.employeeName,
                        employeeEmail = employeeVM.employeeEmail,
                        managerId = managerId
                    };
                    _dbContext.employees.Add(employee);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in Add Employee Method : " + ex.Message);
                return false;
            }
        }

        public int GetEmployeeId(string employeeEmail)
        {
            var employee = _dbContext.employees.Where(e => e.employeeEmail == employeeEmail).FirstOrDefault();
            return employee.employeeId;
        }

        public List<Employee> GetEmployeeListByManagerId(int managerId)
        {
            var employeeList = _dbContext.employees.Where(e => e.managerId == managerId).ToList();
            return employeeList;
        }

        public bool IsEmployeeEmailExist(string employeeEmail)
        {
            var employee = _dbContext.employees.Where(e => e.employeeEmail == employeeEmail).FirstOrDefault();
            if (employee != null)
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

