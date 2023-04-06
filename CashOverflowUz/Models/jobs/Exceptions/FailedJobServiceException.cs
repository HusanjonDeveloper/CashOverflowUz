//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class FailedJobServiceException : Xeption
	{
		public FailedJobServiceException(Exception innerException)
			: base(message: "Failed job service error occurred, please contact support.", innerException)
		{ }
	}
}
