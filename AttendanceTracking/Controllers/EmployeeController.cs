using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracking.Data.Constants;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Services;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddEmployee([FromForm] EmployeeVM employeeVM)
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

        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var employees = _employeeService.GetAllEmployees();
            if (employees != null)
            {
                return Ok(employees);
            }
            else
            {
                return NotFound(ResponseConstants.NoEmployeesFound);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetEmployeeIdByEmployeeEmail(string employeeEmail)
        {
            var employeeId = _employeeService.GetEmployeeIdByEmployeeEmail(employeeEmail);
            if (employeeId != 0)
            {
                return Ok(ResponseConstants.EmployeeId + employeeId);
            }
            else
            {
                return NotFound(ResponseConstants.EmployeeNotFound);
            }
        }
    }
}
