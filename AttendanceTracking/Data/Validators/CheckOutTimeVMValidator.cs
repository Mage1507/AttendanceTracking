using System;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Services;
using FluentValidation;

namespace AttendanceTracking.Data.Validators
{
    public class CheckOutTimeVMValidator : AbstractValidator<CheckOutTimeVM>
    {
        public CheckOutTimeVMValidator()
        {
            RuleFor(c => c.employeeId).NotEmpty().WithMessage("Employee email is required");
        }
    }
}
