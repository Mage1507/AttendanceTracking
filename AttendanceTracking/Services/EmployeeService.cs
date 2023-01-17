using System;
using AttendanceTracking.Data;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AutoMapper;

namespace AttendanceTracking.Services
{
    public class EmployeeService
    {
        private DbInitializer _dbContext;

        private ManagerService _managerService;

        private readonly IMapper _mapper;

        private readonly ILogger<EmployeeService> _logger;
        public EmployeeService(DbInitializer dbContext, ManagerService managerService, ILogger<EmployeeService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _managerService = managerService;
            _logger = logger;
            _mapper = mapper;
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

                    var mappedEmployee = _mapper.Map<Employee>(employeeVM);
                    _dbContext.employees.Add(mappedEmployee);
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

