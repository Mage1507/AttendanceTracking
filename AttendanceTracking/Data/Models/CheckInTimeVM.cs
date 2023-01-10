﻿using System;
using System.ComponentModel.DataAnnotations;

namespace AttendanceTracking.Data.Models
{
    public class CheckInTimeVM
    {

        public DateTime date { get; set; }
        public DateTime checkInTime { get; set; }
        public string employeeEmail { get; set; }
    }
}

