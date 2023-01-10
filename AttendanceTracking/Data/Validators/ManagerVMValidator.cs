using System;
using AttendanceTracking.Data.Models;
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
			RuleFor(c => c.managerEmail).NotEmpty().WithMessage("Manager email is required").EmailAddress().WithMessage("Manager email is not valid").Must(UniqueEmail).WithMessage("Manager email already exist");
			RuleFor(c => c.departmentName).NotEmpty().WithMessage("Department name is required");
		}

		public bool UniqueEmail(string managerEmail)
		{

			Console.WriteLine("Manager Email : " + managerEmail);
			if (_managerService.IsManagerEmailExist(managerEmail))
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

