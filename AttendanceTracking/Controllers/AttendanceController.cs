using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceTracking.Data.Constants;
using AttendanceTracking.Data.ResponseModels;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AttendanceTracking.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AttendanceController : Controller
    {
        private readonly AttendanceService _attendanceService;

        private readonly ILogger<AttendanceController> _logger;

        public AttendanceController(
            AttendanceService attendanceService,
            ILogger<AttendanceController> logger
        )
        {
            _attendanceService = attendanceService;
            _logger = logger;
        }

        [Route("[Action]")]
        [HttpPost]
        public IActionResult CheckInLog([FromBody] CheckInTimeVM checkInTimeVM)
        {
            _logger.LogInformation("CheckInLog method called : " + checkInTimeVM);
            var checkInLog = _attendanceService.LogCheckIn(checkInTimeVM);
            if (checkInLog)
            {
                return Ok(ResponseConstants.LogCheckInSuccessfully);
            }
            else
            {
                return NotFound(ResponseConstants.LogCheckInNotSuccessfully);
            }
        }

        [Route("[Action]")]
        [HttpPut]
        public IActionResult CheckOutLog([FromBody] CheckOutTimeVM checkOutTimeVM)
        {
            _logger.LogInformation("CheckOutLog method called : " + checkOutTimeVM);
            var checkOutLog = _attendanceService.LogCheckOut(checkOutTimeVM);
            if (checkOutLog)
            {
                return Ok(ResponseConstants.LogCheckOutSuccessfully);
            }
            else
            {
                return NotFound(ResponseConstants.LogCheckOutNotSuccessfully);
            }
        }

        [Route("[Action]/{managerId}/{date}/{fromTime}/{toTime}")]
        [HttpGet]
       
        public List<AttendanceResponse> GetAttendanceOfEmployee(
            int managerId,
            DateTime date,
            DateTime fromTime,
            DateTime toTime
        )
        {
            _logger.LogInformation(
                "GetAttendanceOfEmployee method called : "
                    + managerId
                    + " "
                    + date
                    + " "
                    + fromTime
                    + " "
                    + toTime
            );
            try
            {
                var attendance = _attendanceService.GetAttendanceOfEmployee(
                    managerId,
                    date,
                    fromTime,
                    toTime
                );
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
