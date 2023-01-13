using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using Microsoft.AspNetCore.Mvc;



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
        public IActionResult CheckInLog([FromBody] CheckInTimeVM checkInTimeVM)
        {
            _logger.LogInformation("CheckInLog method called");
            var checkInLog = _attendanceService.LogCheckIn(checkInTimeVM);
            if (checkInLog)
            {
                return Ok("CheckInTime Logged Successfully");
            }
            else
            {
                return NotFound("Check Email or Already CheckInTime");
            }
        }

        [Route("[Action]")]
        [HttpPut]
        public IActionResult CheckOutLog([FromBody] CheckOutTimeVM checkOutTimeVM)
        {
            _logger.LogInformation("CheckOutLog method called");
            var checkOutLog = _attendanceService.LogCheckOut(checkOutTimeVM);
            if (checkOutLog)
            {
                return Ok("CheckOutTime Logged Successfully");
            }
            else
            {
                return NotFound("Check Email or No CheckInTime");
            }
        }

        [Route("[Action]/{managerEmail}/{date}/{fromTime}/{toTime}")]
        [HttpGet]
        public List<Attendance> GetAttendanceOfEmployee(string managerEmail, DateTime date, DateTime fromTime, DateTime toTime)
        {
            _logger.LogInformation("GetAttendanceOfEmployee method called");
            try
            {
                var attendance = _attendanceService.GetAttendanceOfEmployee(managerEmail, date, fromTime, toTime);
                return attendance;
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return null;
            }

        }

    }
}

