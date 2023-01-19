using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AttendanceTracking.Data.Constants;

namespace AttendanceTracking.Controllers
{
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentService _departmentService;
        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {

            var addDepartment = _departmentService.AddDepartment(department);

            if (addDepartment)
            {
                return Ok(ResponseConstants.DepartmentAddedSuccessfully);
            }
            else
            {
                return NotFound(ResponseConstants.DepartmentNotAdded);
            }

        }

    }
}

