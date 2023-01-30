using System;
using Microsoft.EntityFrameworkCore;
using AttendanceTracking.Models;

namespace AttendanceTracking.Data
{
    public class DbInitializer : DbContext
    {
        public DbInitializer(DbContextOptions<DbInitializer> options)
            : base(options) { }

        public DbSet<Employee> employees { get; set; }

        public DbSet<Manager> managers { get; set; }

        public DbSet<Department> departments { get; set; }

        public DbSet<Attendance> attendances { get; set; }
    }
}
