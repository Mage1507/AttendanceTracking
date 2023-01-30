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


        [Required]
        public string employeeName { get; set; }


        [Required]
        [EmailAddress]
        public string employeeEmail { get; set; }


        [Required]
        public string profileImageUrl { get; set; }

        public int managerId { get; set; }

        public Manager manager { get; set; }

    }
}

