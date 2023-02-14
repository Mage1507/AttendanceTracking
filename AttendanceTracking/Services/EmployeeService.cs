using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AttendanceTracking.Data;
using AttendanceTracking.Data.Constants;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AutoMapper;
using AwsS3.Models;
using AwsS3.Services;
using AwsSecretManager.Interface;
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

        private readonly IConfigSettings _configSettings;       

        private readonly IStorageService _storageService;



        public EmployeeService(
            DbInitializer dbContext,
            ManagerService managerService,
            ILogger<EmployeeService> logger,
            IMapper mapper,
            IStorageService storageService,
            IConfigSettings configSettings
        )
        {
            _dbContext = dbContext;
            _managerService = managerService;
            _logger = logger;
            _mapper = mapper;
            _storageService = storageService;
            _configSettings = configSettings;
        }

        public EmployeeService()
        {
            
        }

        // Add Employee
        public async Task<bool> AddEmployee(EmployeeVM employeeVM)
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


                    await using var memoryStream = new MemoryStream();
                    await employeeVM.profileImageUrl.CopyToAsync(memoryStream);

                    var fileExt = Path.GetExtension(employeeVM.profileImageUrl.FileName);
                    var docName = $"{employeeVM.employeeEmail.Split('@')[0]}{fileExt}";
                    // call server

                    var s3Obj = new S3Object()
                    {
                        BucketName = "dotnet-training-attendancetracking",
                        InputStream = memoryStream,
                        Name = docName
                    };

                    var cred = new AwsCredentials()
                    {
                        AccessKey = _configSettings.AwsAccessKey,
                        SecretKey = _configSettings.AwsSecretKey,
                        SessionToken = _configSettings.AwsSessionToken
                    };
                    _logger.LogInformation("credentials" + cred.AccessKey + cred.SecretKey + cred.SessionToken);
                    var result = await _storageService.UploadFileAsync(s3Obj, cred);
                    _logger.LogInformation("UploadFileAsync Method Called" + result);
                    mappedEmployee.profileImageUrl = ResponseConstants.ImageUrl+docName;
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
        public virtual List<EmployeeResponse> GetAllEmployees()
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
        public virtual int GetEmployeeIdByEmployeeEmail(string employeeEmail)
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
