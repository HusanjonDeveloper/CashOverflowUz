//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{
	public class FailedSalaryServiceException : Xeption
	{
		public FailedSalaryServiceException(Exception innerException)
			: base(message: "Failed salary service error occurred, contact support.", innerException)
		{ }
	}
}
