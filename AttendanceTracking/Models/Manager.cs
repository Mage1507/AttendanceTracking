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

        [Required]
        public string managerName { get; set; }

        [Required]
        [EmailAddress]
        public string managerEmail { get; set; }

        [Required]
        public string managerPassword { get; set; }

        public int departmentId { get; set; }

        public Department department { get; set; }
    }
}

