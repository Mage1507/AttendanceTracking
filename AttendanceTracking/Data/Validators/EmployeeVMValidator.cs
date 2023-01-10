using System;
using AttendanceTracking.Data.Models;
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
            RuleFor(c => c.employeeEmail).NotEmpty().WithMessage("Employee email is required").EmailAddress().WithMessage("Employee email is not valid").Must(UniqueEmail).WithMessage("Employee email already exist");
            RuleFor(c => c.managerEmail).NotEmpty().WithMessage("Manager email is required").EmailAddress().WithMessage("Manager email is not valid");
        }
        public bool UniqueEmail(string employeeEmail)
        {
            if (_employeeService.IsEmployeeEmailExist(employeeEmail))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}

