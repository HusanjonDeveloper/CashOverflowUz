//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{
	public class SalaryDependencyValidationException : Xeption
	{
		public SalaryDependencyValidationException(Xeption innerException)
			: base(message: "Salary dependency validation error occured, fix the errors and try again.", innerException)
		{ }

	 }
}
