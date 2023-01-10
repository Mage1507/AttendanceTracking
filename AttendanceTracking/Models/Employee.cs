using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracking.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int employeeId { get; set; }

        public string employeeName { get; set; }

        [EmailAddress]
        public string employeeEmail { get; set; }

        public int managerId { get; set; }

        public Manager manager { get; set; }

    }
}

