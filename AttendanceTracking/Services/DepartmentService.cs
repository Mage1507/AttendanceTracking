using System;
using System.Collections.Generic;
using System.Linq;
using AttendanceTracking.Data;
using AttendanceTracking.Models;
using Microsoft.Extensions.Logging;

namespace AttendanceTracking.Services
{
    public class DepartmentService
    {
        private readonly DbInitializer _dbContext;

        private readonly ILogger<DepartmentService> _logger;

        public DepartmentService()
        {
            
        }

        public DepartmentService(DbInitializer dbContext, ILogger<DepartmentService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // Add Department
        public virtual bool AddDepartment(Department department)
        {
            _logger.LogInformation("AddDepartment Method Called" + department);
            try
            {
                if (IsDepartmentNameExist(department.departmentName) | department == null)
                {
                    return false;
                }
                else
                {
                    _dbContext.departments.Add(department);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in Add Department Method : " + ex.Message);
                return false;
            }
        }

        //Get All Departments
        public virtual List<Department> GetAllDepartments()
        {
            _logger.LogInformation("GetAllDepartments Method Called");
            try
            {
                var departments = _dbContext.departments.ToList();
                if (departments != null)
                {
                    return departments;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetAllDepartments Method : " + ex.Message);
                return null;
            }
        }

        //Check whether Department Name Already Exist
        public bool IsDepartmentNameExist(string departmentName)
        {
            var department = _dbContext.departments
                .Where(d => d.departmentName == departmentName)
                .FirstOrDefault();
            if (department != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //Get Department Id By Name
        public int GetDepartmentIdByName(string departmentName)
        {
            _logger.LogInformation("GetDepartmentIdByName Method Called");
            try
            {
                var department = _dbContext.departments
                    .Where(d => d.departmentName == departmentName)
                    .FirstOrDefault();
                if (department != null)
                {
                    return department.departmentId;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetDepartmentIdByName Method : " + ex.Message);
                return 0;
            }
        }
    }
}
