using System;
using AttendanceTracking.Data;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Models;

namespace AttendanceTracking.Services
{
    public class AttendanceService
    {

        private readonly DbInitializer _dbContext;

        private readonly EmployeeService _employeeService;

        private readonly ManagerService _managerService;

        private readonly ILogger<AttendanceService> _logger;

        public AttendanceService(DbInitializer dbContext, EmployeeService employeeService, ManagerService manager, ILogger<AttendanceService> logger)
        {
            _dbContext = dbContext;
            _employeeService = employeeService;
            _managerService = manager;
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
                var attendanceList = GetAttendanceListByEmployeeId(checkInTimeVM.employeeId);
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
                        employeeId = checkInTimeVM.employeeId,
                        date = DateTime.Now.Date,
                        checkInTime = DateTime.UtcNow
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
              
                var attendanceList = GetAttendanceListByEmployeeId(checkOutTimeVM.employeeId);
                foreach (var att in attendanceList)
                {
                    string date = att.date.ToShortDateString();
                    if (date == DateTime.Now.ToShortDateString() && att.checkOutTime == null)
                    {

                        att.checkOutTime = DateTime.UtcNow;
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


        public List<Attendance> GetAttendanceOfEmployee(int managerId, DateTime date, DateTime fromTime, DateTime toTime)
        {
            var employeeList = GetAttendanceListByManagerId(managerId);
            var attendanceList = new List<Attendance>();
            //get attendance list by employee id
            foreach (var emp in employeeList)
            {
                TimeSpan? ts = new TimeSpan(0, 0, 0);
                var attendance = GetAttendanceListByEmployeeId(emp.employeeId);
                foreach (var att in attendance)
                {
                    _logger.LogInformation("CheckOut Time:" + att.checkOutTime?.ToLocalTime() + "CheckIn Time:" + att.checkInTime.ToLocalTime());

                    if (att.date == date && att.checkInTime.ToLocalTime() >= fromTime && att.checkOutTime?.ToLocalTime() <= toTime)
                    {
                        att.totalPresentTime = att.checkOutTime?.ToLocalTime() - att.checkInTime.ToLocalTime();
                        _logger.LogInformation("Total Present Time:" + att.totalPresentTime);
                         ts = ts+att.totalPresentTime;
                        _logger.LogInformation("Total Present Time:" + ts);
                        att.totalHoursInOffice = ts;
                        attendanceList.Add(att);
                    }
                   

                }
            }

            return attendanceList;

        }
        public List<Employee> GetAttendanceListByManagerId(int managerId)
        {
            var employeeList = _employeeService.GetEmployeeListByManagerId(managerId);
            return employeeList;
        }

        public List<Attendance> GetAttendanceListByEmployeeId(int employeeId)
        {
            var attendanceList = _dbContext.attendances.Where(a => a.employeeId == employeeId).ToList();
            return attendanceList;
        }

    }
}

