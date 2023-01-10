using System;
using AttendanceTracking.Data.Models;
using AttendanceTracking.Services;
using FluentValidation;

namespace AttendanceTracking.Data.Validators
{
    public class CheckInTimeVMValidator : AbstractValidator<CheckInTimeVM>
    {
        public CheckInTimeVMValidator()
        {
            RuleFor(c => c.date).NotEmpty().WithMessage("Check in time is required").Equal(DateTime.Now.Date).WithMessage("Check in date and time must be today");
            RuleFor(c => c.checkInTime).NotEmpty().WithMessage("Check in time is required");
            RuleFor(c => c.employeeEmail).NotEmpty().WithMessage("Employee email is required").EmailAddress().WithMessage("Employee email is not valid");

        }


    }
}

