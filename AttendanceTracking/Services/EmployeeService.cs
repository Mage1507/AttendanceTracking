using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AttendanceTracking.Data;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AutoMapper;
using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AttendanceTracking.Services
{
    public class EmployeeService
    {
        private DbInitializer _dbContext;

        private ManagerService _managerService;

        private readonly IMapper _mapper;

        private readonly ILogger<EmployeeService> _logger;



        public EmployeeService(
            DbInitializer dbContext,
            ManagerService managerService,
            ILogger<EmployeeService> logger,
            IMapper mapper,
            IConfiguration configuration
        )
        {
            _dbContext = dbContext;
            _managerService = managerService;
            _logger = logger;
            _mapper = mapper;
        }

        // Add Employee
        public bool AddEmployee(EmployeeVM employeeVM)
        {
            _logger.LogInformation("AddEmployee Method Called" + employeeVM);
            if (employeeVM == null)
            {
                return false;
            }
            try
            {
                if (
                    IsEmployeeEmailExist(employeeVM.employeeEmail)
                    | IsEmployeeEmailExistsInManager(employeeVM.employeeEmail)
                )
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

        //Get All Employees
        public List<EmployeeResponse> GetAllEmployees()
        {
            try
            {
                var employees = _mapper.Map<List<EmployeeResponse>>(_dbContext.employees.Include("manager").ToList());
                if (employees != null)
                {
                    return employees;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in Get All Employees Method : " + ex.Message);
                return null;
            }
        }

        //Get Employee List By Manager Id
        public List<Employee> GetEmployeeListByManagerId(int managerId)
        {
            var employeeList = _dbContext.employees.Where(e => e.managerId == managerId).ToList();
            return employeeList;
        }


        //Check Whether Employee Email Already Exists or Not
        public bool IsEmployeeEmailExist(string employeeEmail)
        {
            _logger.LogInformation("IsEmployeeEmailExist Method Called" + employeeEmail);
            var employee = _dbContext.employees
                .Where(e => e.employeeEmail == employeeEmail)
                .FirstOrDefault();
            if (employee != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get Employee Id By Employee Email
        public int GetEmployeeIdByEmployeeEmail(string employeeEmail)
        {
            _logger.LogInformation("GetEmployeeIdByEmployeeEmail Method Called" + employeeEmail);
            var employee = _dbContext.employees
                .Where(e => e.employeeEmail == employeeEmail)
                .FirstOrDefault();
            if (employee != null)
            {
                return employee.employeeId;
            }
            else
            {
                return 0;
            }
        }

        //Get Employee Email By Employee Id
        public string GetEmployeeEmailById(int employeeId)
        {
            _logger.LogInformation("GetEmployeeEmailById Method Called" + employeeId);
            var employee = _dbContext.employees
                .Where(e => e.employeeId == employeeId)
                .FirstOrDefault();
            if (employee != null)
            {
                return employee.employeeEmail;
            }
            else
            {
                return null;
            }
        }

        //Check whether Employee Email Already Exists in Manager Table or Not
        public bool IsEmployeeEmailExistsInManager(string employeeEmail)
        {
            _logger.LogInformation("IsEmployeeEmailExistsInManager Method Called" + employeeEmail);
            var manager = _dbContext.managers
                .Where(m => m.managerEmail == employeeEmail)
                .FirstOrDefault();
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
