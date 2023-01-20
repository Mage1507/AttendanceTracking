using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AttendanceTracking.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using AttendanceTracking.Data.Constants;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AttendanceTracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : Controller
    {
        private readonly ManagerService _managerService;
        public ManagerController(ManagerService managerService)
        {
            _managerService = managerService;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult AddManager([FromBody]ManagerVM managerVM)
        {
            var addManager = _managerService.AddManager(managerVM);
            if (addManager)
            {
                return Ok(ResponseConstants.ManagerAddedSuccessfully);
            }
            else
            {
                return NotFound(ResponseConstants.ManagerNotAdded);
            }
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult ManagerLogin([FromBody]ManagerLoginVM managerLoginVM)
        {
            var manager = _managerService.ManagerLogin(managerLoginVM);
            if (manager != null)
            {
                return Ok(manager);
            }
            else
            {
                return NotFound(ResponseConstants.ManagerNotFound);
            }
        }

        [Route("[Action]")]
        [HttpGet]
        public IActionResult GetAllManagers()
        {
            var managers = _managerService.GetAllManagers();
            if (managers != null)
            {
                return Ok(managers);
            }
            else
            {
                return NotFound(ResponseConstants.NoManagersFound);
            }
        }
    }
}

