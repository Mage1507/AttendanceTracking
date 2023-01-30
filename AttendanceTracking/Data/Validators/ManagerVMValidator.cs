using System;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Services;
using FluentValidation;

namespace AttendanceTracking.Data.Validators
{
    public class ManagerVMValidator : AbstractValidator<ManagerVM>
    {
        private readonly ManagerService _managerService;

        public ManagerVMValidator(ManagerService managerService)
        {
            _managerService = managerService;
            RuleFor(c => c.managerName).NotEmpty().WithMessage("Manager name is required");
            RuleFor(c => c.managerEmail)
                .NotEmpty()
                .WithMessage("Manager email is required")
                .EmailAddress()
                .WithMessage("Manager email is not valid");
            RuleFor(c => c.managerPassword)
                .NotEmpty()
                .WithMessage("Password Field cannot be empty")
                .MinimumLength(8)
                .WithMessage("Your password length must be at least 8.")
                .Matches(@"[A-Z]+")
                .WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+")
                .WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+")
                .WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+")
                .WithMessage("Your password must contain at least one (!? *.).");
            RuleFor(c => c.departmentId).NotEmpty().WithMessage("Department id is required");
        }
    }
}
