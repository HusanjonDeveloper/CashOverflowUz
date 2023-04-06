//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{
	public class InvalidSalaryException : Xeption
	{
		public InvalidSalaryException()
			: base(message: "Salary is invalid.")
		{ }
	}
}
