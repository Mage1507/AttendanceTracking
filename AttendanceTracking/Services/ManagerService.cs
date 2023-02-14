using System;
using AttendanceTracking.Data;
using AttendanceTracking.Models;
using AttendanceTracking.Data.ViewModels;
using AutoMapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AttendanceTracking.Data.ResponseModels;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;

namespace AttendanceTracking.Services
{
    public class ManagerService
    {
        private readonly DbInitializer _dbContext;

        public DepartmentService _departmentService;

        private readonly IMapper _mapper;

        private readonly ILogger<ManagerService> _logger;

        public IConfiguration _configuration;

        public ManagerService(
            DbInitializer dbContext,
            DepartmentService departmentService,
            ILogger<ManagerService> logger,
            IMapper mapper,
            IConfiguration configuration
        )
        {
            _dbContext = dbContext;
            _departmentService = departmentService;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
        }

        public ManagerService()
        {
            
        }

        // Add Manager
        public virtual bool AddManager(ManagerVM managerVM)
        {
            _logger.LogInformation("Add Manager Method Called");
            try
            {
                if (
                    IsManagerEmailExist(managerVM.managerEmail)
                    | IsManagerEmailExistInEmployee(managerVM.managerEmail)
                    | managerVM == null
                )
                {
                    return false;
                }
                else
                {
                    var mappedManager = _mapper.Map<Manager>(managerVM);
                    var encData_byte = System.Text.Encoding.UTF8.GetBytes(
                        mappedManager.managerPassword
                    );
                    mappedManager.managerPassword = Convert.ToBase64String(encData_byte);
                    _dbContext.managers.Add(mappedManager);
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

        //Authentication for accessing GetAttendanceOfEmployee API
        public string ManagerLogin(ManagerLoginVM managerLoginVM)
        {
            _logger.LogInformation("ManagerLogin Method Called");
            try
            {
                var encData_byte = System.Text.Encoding.UTF8.GetBytes(
                    managerLoginVM.managerPassword
                );
                managerLoginVM.managerPassword = Convert.ToBase64String(encData_byte);

                var manager = _dbContext.managers
                    .Where(
                        m =>
                            m.managerEmail == managerLoginVM.managerEmail
                            && m.managerPassword == managerLoginVM.managerPassword
                    )
                    .FirstOrDefault();
                if (manager != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("ManagerId", manager.managerId.ToString()),
                        new Claim("ManagerName", manager.managerName),
                        new Claim("ManagerEmail", manager.managerEmail),
                        new Claim("ManagerPassword", manager.managerPassword),
                    };
                    var key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                    );
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                    );

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in ManagerLogin Method : " + ex.Message);
                return null;
            }
        }

        //Get All Managers

        public virtual List<ManagerResponse> GetAllManagers()
        {
            _logger.LogInformation("GetAllManagers Method Called");
            try
            {
                var managers = _mapper.Map<List<ManagerResponse>>(_dbContext.managers.ToList());
                if (managers != null)
                {
                    return managers;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetAllManagers Method : " + ex.Message);
                return null;
            }
        }

        //Check whether manager email already exist or not
        public bool IsManagerEmailExist(string managerEmail)
        {
            _logger.LogInformation("IsManagerEmailExist Method Called");
            var manager = _dbContext.managers
                .Where(m => m.managerEmail == managerEmail)
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

        //Get Manager Id by Manager Email
        public virtual int GetManagerIdByManagerEmail(string managerEmail)
        {
            _logger.LogInformation("GetManagerIdByEmail Method Called");
            var manager = _dbContext.managers
                .Where(m => m.managerEmail == managerEmail)
                .FirstOrDefault();
            if (manager != null)
            {
                return manager.managerId;
            }
            else
            {
                return 0;
            }
        }

        //Get Manager Email by Manager Id
        public string GetManagerEmail(int managerId)
        {
            _logger.LogInformation("GetManagerEmail Method Called");
            var manager = _dbContext.managers.Where(m => m.managerId == managerId).FirstOrDefault();
            if (manager != null)
            {
                return manager.managerEmail;
            }
            else
            {
                return null;
            }
        }

        //Check whether manager email already exist in employee table or not
        public bool IsManagerEmailExistInEmployee(string managerEmail)
        {
            _logger.LogInformation("IsManagerEmailExistInEmployee Method Called");
            var employee = _dbContext.employees
                .Where(e => e.employeeEmail == managerEmail)
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
    }
}
