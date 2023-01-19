using System;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Services;
using FluentValidation;

namespace AttendanceTracking.Data.Validators
{
    public class EmployeeVMValidator : AbstractValidator<EmployeeVM>
    {
        private readonly EmployeeService _employeeService;

        public EmployeeVMValidator(EmployeeService employeeService)
        {

            _employeeService = employeeService;
            RuleFor(c => c.employeeName).NotEmpty().WithMessage("Employee name is required");
            RuleFor(c => c.employeeEmail).NotEmpty().WithMessage("Employee email is required").EmailAddress().WithMessage("Employee email is not valid");
            RuleFor(c => c.managerId).NotEmpty().WithMessage("Manager id is required");
        }
    }
}

