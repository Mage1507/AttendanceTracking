﻿using System;
using AttendanceTracking.Data.Models;
using AttendanceTracking.Services;
using FluentValidation;

namespace AttendanceTracking.Data.Validators
{
    public class CheckOutTimeVMValidator : AbstractValidator<CheckOutTimeVM>
    {


        public CheckOutTimeVMValidator()
        {
    
            RuleFor(c => c.employeeEmail).NotEmpty().WithMessage("Employee email is required").EmailAddress().WithMessage("Employee email is not valid");

        }

    
    }
}

