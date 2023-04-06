//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{
	public class AlreadyExistsSalaryException : Xeption
	{
		public AlreadyExistsSalaryException(Exception innerException)
			: base(message: "Salary already exists.", innerException)
		{ }
	}
}
