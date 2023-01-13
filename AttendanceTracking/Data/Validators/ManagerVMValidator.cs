using System;
using AttendanceTracking.Data.ViewModels;
using AttendanceTracking.Services;
using FluentValidation;

namespace AttendanceTracking.Data.Validators
{
	public class ManagerVMValidator:AbstractValidator<ManagerVM>
	{

		private readonly ManagerService _managerService;
		public ManagerVMValidator(ManagerService managerService)
		{
			_managerService = managerService;
			RuleFor(c => c.managerName).NotEmpty().WithMessage("Manager name is required");
            RuleFor(c => c.managerEmail).NotEmpty().WithMessage("Manager email is required").EmailAddress().WithMessage("Manager email is not valid");
			RuleFor(c => c.departmentName).NotEmpty().WithMessage("Department name is required");
        }

	
		
	}
}

