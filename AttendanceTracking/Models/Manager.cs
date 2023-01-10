using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AttendanceTracking.Models
{
    public class Manager
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int managerId { get; set; }

        public string managerName { get; set; }

        [EmailAddress]
        public string managerEmail { get; set; }

        public int departmentId { get; set; }

        public Department department { get; set; }
    }
}

