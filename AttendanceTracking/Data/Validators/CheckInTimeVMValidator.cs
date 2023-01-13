using System;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Services;
using FluentValidation;

namespace AttendanceTracking.Data.Validators
{
    public class CheckInTimeVMValidator : AbstractValidator<CheckInTimeVM>
    {
        public CheckInTimeVMValidator()
        {
           
            RuleFor(c => c.employeeEmail).NotEmpty().WithMessage("Employee email is required").EmailAddress().WithMessage("Employee email is not valid");
        }


    }
}

