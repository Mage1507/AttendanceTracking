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
           
            RuleFor(c => c.employeeId).NotEmpty().WithMessage("Employee id is required");
        }


    }
}

