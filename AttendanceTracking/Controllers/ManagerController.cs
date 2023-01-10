﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AttendanceTracking.Data.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AttendanceTracking.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ManagerService _managerService;
        public ManagerController(ManagerService managerService)
        {
            _managerService = managerService;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddManager([FromBody]ManagerVM manager)
        {
            var addManager = _managerService.AddManager(manager);
            if (addManager)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}

