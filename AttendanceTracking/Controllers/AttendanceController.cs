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
    public class AttendanceController : Controller
    {

        private readonly AttendanceService _attendanceService;

        private readonly ILogger<AttendanceController> _logger;
        public AttendanceController(AttendanceService attendanceService, ILogger<AttendanceController> logger)
        {
            _attendanceService = attendanceService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CheckInLog([FromBody]CheckInTimeVM checkInTimeVM)
        {
            _logger.LogInformation("CheckInLog method called");
            var checkInLog = _attendanceService.LogCheckIn(checkInTimeVM);
            if (checkInLog)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [Route("[Action]")]
        [HttpPut]
        public IActionResult CheckOutLog([FromBody]CheckOutTimeVM checkOutTimeVM)
        {
            _logger.LogInformation("CheckOutLog method called");
            var checkOutLog = _attendanceService.LogCheckOut(checkOutTimeVM);
            if (checkOutLog)
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

