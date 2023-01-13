using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracking.Data.Models;
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
        public IActionResult AddEmployee(EmployeeVM employeeVM)
        {
            var addEmployee = _employeeService.AddEmployee(employeeVM);
            if (addEmployee)
            {
                return Ok("Employee Added Successfully");
            }
            else
            {
                return NotFound("Employee Already Exists or Check Manager Email");
            }
        }
    }
}

