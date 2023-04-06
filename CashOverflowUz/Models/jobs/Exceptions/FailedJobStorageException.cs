//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class FailedJobStorageException : Xeption
	{
		public FailedJobStorageException(Exception innerException)
			: base(message: "Failed user storage error occurred, contact support.", innerException)
		{ }
	}
}
