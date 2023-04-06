//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.jobs.Exceptions
{
	public class JobValidationException : Xeption
	{
		public JobValidationException(Xeption innerException)
			: base(message: "Job validation error occured, fix the errors and try again.", innerException)
		{ }
	}
}
