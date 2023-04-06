//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class JobDependencyException : Xeption
	{
		public JobDependencyException(Exception innerException)
			: base(message: "Job dependency error occured, contact support.", innerException)
		{ }
	}
}
