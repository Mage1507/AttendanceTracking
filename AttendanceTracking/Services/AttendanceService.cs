using System;
using AttendanceTracking.Data;
using AttendanceTracking.Data.Models;
using AttendanceTracking.Models;

namespace AttendanceTracking.Services
{
    public class AttendanceService
    {

        private readonly DbInitializer _dbContext;

        private readonly EmployeeService _employeeService;

        private readonly ILogger<AttendanceService> _logger;

        public AttendanceService(DbInitializer dbContext, EmployeeService employeeService, ILogger<AttendanceService> logger)
        {
            _dbContext = dbContext;
            _employeeService = employeeService;
            _logger = logger;
        }

        public bool LogCheckIn(CheckInTimeVM checkInTimeVM)
        {
            int count = 0;
            if (checkInTimeVM == null)
            {
                return false;
            }
            try
            {
                var employeeId = _employeeService.GetEmployeeId(checkInTimeVM.employeeEmail);
                var attendanceList = GetAttendanceListByEmployeeId(employeeId);
                foreach (var att in attendanceList)
                {
                    string date = att.date.ToShortDateString();
                    if (date == DateTime.Now.ToShortDateString() && att.checkOutTime == null)
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    Attendance attendance = new Attendance()
                    {
                        employeeId = employeeId,
                        date = checkInTimeVM.date,
                        checkInTime = checkInTimeVM.checkInTime,
                    };
                    _dbContext.attendances.Add(attendance);
                    _dbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in LogCheckIn Method :" + ex.Message);
                return false;
            }
        }

        public bool LogCheckOut(CheckOutTimeVM checkOutTimeVM)
        {
            int count = 0;
            if (checkOutTimeVM == null)
            {
                return false;
            }
            try
            {
                var employeeId = _employeeService.GetEmployeeId(checkOutTimeVM.employeeEmail);
                var attendanceList = GetAttendanceListByEmployeeId(employeeId);
                foreach (var att in attendanceList)
                {
                    string date = att.date.ToShortDateString();
                    if (date == DateTime.Now.ToShortDateString() && att.checkOutTime == null)
                    {
                        att.checkOutTime = checkOutTimeVM.checkOutTime;
                        _dbContext.attendances.Update(att);
                        _dbContext.SaveChanges();
                        count++;
                        break;
                    }
                }
                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("Exception:" + ex.Message);
                return false;
            }
        }

        public List<Attendance> GetAttendanceListByEmployeeId(int employeeId)
        {
            var attendanceList = _dbContext.attendances.Where(a => a.employeeId == employeeId).ToList();
            return attendanceList;
        }

    }
}

