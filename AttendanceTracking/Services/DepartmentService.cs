﻿using System;
using AttendanceTracking.Data;
using AttendanceTracking.Models;
namespace AttendanceTracking.Services
{
    public class DepartmentService
    {
        private readonly DbInitializer _dbContext;

        private readonly ILogger<DepartmentService> _logger;
        public DepartmentService(DbInitializer dbContext, ILogger<DepartmentService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public bool AddDepartment(Department department)
        {
            if (department == null)
            {
                return false;
            }
            try
            {
                if (IsDepartmentNameExist(department.departmentName))
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

        public List<Department> GetAllDepartments()
        {
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



        public bool IsDepartmentNameExist(string departmentName)
        {
            var department = _dbContext.departments.Where(d => d.departmentName == departmentName).FirstOrDefault();
            if (department != null)
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

