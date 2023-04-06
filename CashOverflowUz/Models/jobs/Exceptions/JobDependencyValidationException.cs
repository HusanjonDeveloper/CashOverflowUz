//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class JobDependencyValidationException : Xeption
	{
		public JobDependencyValidationException(Xeption innerException)
			: base(message: "Job dependency validation error occurred, fix the errors and try again.",
				  innerException)
		{ }
	}
}
