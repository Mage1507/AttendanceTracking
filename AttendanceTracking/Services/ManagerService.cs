using System;
using AttendanceTracking.Data;
using AttendanceTracking.Models;
using AttendanceTracking.Data.ViewModels;
using AutoMapper;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AttendanceTracking.Services
{
    public class ManagerService
    {
        private readonly DbInitializer _dbContext;

        public DepartmentService _departmentService;

        private readonly IMapper _mapper;

        private readonly ILogger<ManagerService> _logger;

        public IConfiguration _configuration;
        public ManagerService(DbInitializer dbContext, DepartmentService departmentService, ILogger<ManagerService> logger, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _departmentService = departmentService;
            _logger = logger;
            _mapper = mapper;
            _configuration = configuration;
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

                    var mappedManager = _mapper.Map<Manager>(managerVM);
                    var encData_byte = System.Text.Encoding.UTF8.GetBytes(mappedManager.managerPassword);
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

        public string ManagerLogin(ManagerLoginVM managerLoginVM)
        {
            _logger.LogInformation("ManagerLogin Method Called");
            try
            {
                var encData_byte = System.Text.Encoding.UTF8.GetBytes(managerLoginVM.managerPassword);
                managerLoginVM.managerPassword = Convert.ToBase64String(encData_byte);


                var manager = _dbContext.managers.Where(m => m.managerEmail == managerLoginVM.managerEmail && m.managerPassword == managerLoginVM.managerPassword).FirstOrDefault();
                if (manager != null)
                {
                    var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("ManagerId",  manager.managerId.ToString()),
                    new Claim("ManagerName", manager.managerName),
                    new Claim("ManagerEmail", manager.managerEmail),
                    new Claim("ManagerPassword", manager.managerPassword),
                 };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

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

        public List<Manager> GetAllManagers()
        {
            _logger.LogInformation("GetAllManagers Method Called");
            try
            {
                var managers = _dbContext.managers.ToList();
                managers.ForEach(m =>
                {
                    m.managerPassword = null;
                    m.department = _departmentService.GetDepartmentById(m.departmentId);
                });
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

        public bool IsManagerEmailExist(string managerEmail)
        {
            _logger.LogInformation("IsManagerEmailExist Method Called");
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

