//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Salaries.Exceptions
{
	public class FailedSalaryStorageException : Xeption
	{
		public FailedSalaryStorageException(Exception innerException)
			: base(message: "Failed salary storage error occurred, contact support.", innerException)
		{ }
	}
}
