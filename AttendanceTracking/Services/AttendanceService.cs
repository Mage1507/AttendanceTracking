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

        public AttendanceService(DbInitializer dbContext, EmployeeService employeeService)
        {
            _dbContext = dbContext;
            _employeeService = employeeService;
        }

        public bool LogCheckIn(CheckInTimeVM checkInTimeVM)
        {
            if (checkInTimeVM == null)
            {
                return false;
            }
            try
            {
                var employeeId = _employeeService.GetEmployeeId(checkInTimeVM.employeeEmail);
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
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex);
                return false;
            }
        }

        public bool LogCheckOut(CheckOutTimeVM checkOutTimeVM)
        {
            if (checkOutTimeVM == null)
            {
                return false;
            }
            try
            {
                var employeeId = _employeeService.GetEmployeeId(checkOutTimeVM.employeeEmail);
                var attendance = _dbContext.attendances.Find(employeeId);
                if (attendance.checkOutTime == null)
                {
                    attendance.checkOutTime = checkOutTimeVM.checkOutTime;
                    _dbContext.attendances.Update(attendance);
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
                Console.WriteLine("Exception:" + ex);
                return false;
            }
        }

    }
}

