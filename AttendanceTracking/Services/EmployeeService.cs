﻿using System;
using AttendanceTracking.Data;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AutoMapper;
using Azure.Storage.Blobs;

namespace AttendanceTracking.Services
{
    public class EmployeeService
    {
        private DbInitializer _dbContext;

        private ManagerService _managerService;

        private readonly IMapper _mapper;

        private readonly ILogger<EmployeeService> _logger;

        private readonly string _storageConnectionString;

        private readonly string _storageContainerName;

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
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
        }

        // Add Employee
        public bool AddEmployee(EmployeeVM employeeVM)
        {
            _logger.LogInformation("AddEmployee Method Called" + employeeVM);
            if (employeeVM == null)
            {
                return false;
            }
            BlobContainerClient container = new BlobContainerClient(
                _storageConnectionString,
                _storageContainerName
            );
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
                    BlobClient client = container.GetBlobClient(
                        employeeVM.employeeEmail.Split('@')[0]
                            + Path.GetExtension(employeeVM.profileImageUrl.FileName)
                    );

                    using (Stream? data = employeeVM.profileImageUrl.OpenReadStream())
                    {
                        client.Upload(data);
                    }
                    if (client.Uri.AbsoluteUri != null)
                    {
                        var mappedEmployee = _mapper.Map<Employee>(employeeVM);
                        mappedEmployee.profileImageUrl = client.Uri.AbsoluteUri;
                        _dbContext.employees.Add(mappedEmployee);
                        _dbContext.SaveChanges();
                        return true;
                    }
                    return false;
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
                var employees = _mapper.Map<List<EmployeeResponse>>(_dbContext.employees.ToList());
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
