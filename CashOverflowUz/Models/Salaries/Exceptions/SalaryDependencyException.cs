//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{
	public class SalaryDependencyException : Xeption
	{
		public SalaryDependencyException(Exception innerException)
			: base(message: "Salary dependency error occurred, contact support.", innerException)
		{ }
	}
}
