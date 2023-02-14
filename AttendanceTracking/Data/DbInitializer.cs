using System;
using Microsoft.EntityFrameworkCore;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using AwsSecretManager.Interface;

namespace AttendanceTracking.Data
{
    public class DbInitializer : DbContext
    {
        private readonly IConfigSettings _configSettings;
        public DbInitializer(DbContextOptions<DbInitializer> options, IConfigSettings configSettings)
            : base(options)
        {
            _configSettings = configSettings;
        }
        
        public DbInitializer(){}

        public virtual DbSet<Employee> employees { get; set; }

        public virtual DbSet<Manager> managers { get; set; }

        public virtual DbSet<Department> departments { get; set; }

        public virtual DbSet<Attendance> attendances { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configSettings.DbSecret);
        }


    }
}
