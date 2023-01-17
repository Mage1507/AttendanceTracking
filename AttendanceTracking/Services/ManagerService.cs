using System;
using AttendanceTracking.Data;
using AttendanceTracking.Models;
using AttendanceTracking.Data.ViewModels;
using AutoMapper;

namespace AttendanceTracking.Services
{
    public class ManagerService
    {
        private readonly DbInitializer _dbContext;

        public DepartmentService _departmentService;

        private readonly IMapper _mapper;

        private readonly ILogger<ManagerService> _logger;
        public ManagerService(DbInitializer dbContext, DepartmentService departmentService, ILogger<ManagerService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _departmentService = departmentService;
            _logger = logger;
            _mapper = mapper;
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

