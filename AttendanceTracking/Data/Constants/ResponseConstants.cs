using System;

namespace AttendanceTracking.Data.Constants
{
    public static class ResponseConstants
    {
        public const string DepartmentAddedSuccessfully = "Department Added Successfully";

        public const string DepartmentNotAdded = "Error occured in Adding Department";

        public const string NoDepartmentsFound = "Error in fetching Departments";

        public const string DepartmentId = "Department Id is : ";

        public const string ManagerAddedSuccessfully = "Manager Added Successfully";

        public const string ManagerId = "Manager Id is : ";

        public const string ManagerNotAdded = "Error occured in Adding Manager";

        public const string NoManagersFound = "Error in fetching Managers";

        public const string ManagerNotFound = "Manager Not Found";

        public const string DepartmentIdNotFound = "DepartmentId Not Found";

        public const string EmployeeAddedSuccessfully = "Employee Added Successfully";

        public const string EmployeeNotAdded = "Error occured in Adding Employee";

        public const string EmployeeNotFound = "Employee Not Found";

        public const string NoEmployeesFound = "Error in fetching Employees";

        public const string EmployeeId = "Employee Id is : ";

        public const string LogCheckInSuccessfully = "CheckInTime Logged Successfully";

        public const string LogCheckInNotSuccessfully = "Error occured in Logging CheckInTime";

        public const string LogCheckOutSuccessfully = "CheckOutTime Logged Successfully";

        public const string LogCheckOutNotSuccessfully = "Error occured in Logging CheckOutTime";

        public const string ImageUrl = "https://dotnet-training-attendancetracking.s3.amazonaws.com/";
    }
}
