//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{
	public class NullSalaryException : Xeption
	{
		public NullSalaryException()
			: base(message: "Salary is null.")
		{ }
	}
}
