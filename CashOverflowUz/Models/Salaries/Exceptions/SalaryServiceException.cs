//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{

	public class SalaryServiceException : Xeption
	{
		public SalaryServiceException(Exception innerException)
			: base(message: "Salary service error occured, contact support", innerException)
		{ }
	}
}
