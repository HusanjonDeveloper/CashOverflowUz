//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{
	public class SalaryValidationException : Xeption
	{
		public SalaryValidationException(Xeption innerException)
			: base(message: "Salary validation error occurred, fix the errors and try again.", innerException)
		{ }
	}
}
