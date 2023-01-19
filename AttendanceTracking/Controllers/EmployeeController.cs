﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracking.Data.Constants;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AttendanceTracking.Controllers
{
    [ApiController]
    public class EmployeeController : Controller
    {

        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
       
       [Route("[Action]")]
        [HttpPost]
        public IActionResult AddEmployee([FromForm]EmployeeVM employeeVM)
        {
            var addEmployee = _employeeService.AddEmployee(employeeVM);
            if (addEmployee)
            {
                return Ok(ResponseConstants.EmployeeAddedSuccessfully);
            }
            else
            {
                return NotFound(ResponseConstants.EmployeeNotAdded);
            }
        }
    }
}

