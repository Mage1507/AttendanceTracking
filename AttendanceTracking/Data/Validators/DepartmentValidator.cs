using System;
using AttendanceTracking.Models;
using AttendanceTracking.Services;
using FluentValidation;

namespace AttendanceTracking.Data.Validators
{
    public class DepartmentValidator : AbstractValidator<Department>
    {
        private readonly DepartmentService _departmentService;

        public DepartmentValidator(DepartmentService departmentService)
        {
            _departmentService = departmentService;
            RuleFor(c => c.departmentName).NotEmpty().WithMessage("Department name is required");
        }
    }
}
