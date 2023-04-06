//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class JobServiceException : Xeption
	{
		public JobServiceException(Exception innerException)
			: base(message: "Job service error occured, contact support.", innerException)
		{ }
	}
}
