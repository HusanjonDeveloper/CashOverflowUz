//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class ReviewDependencyValidationException : Xeption
	{
		public ReviewDependencyValidationException(Xeption innerException)
			: base(message: "Review dependency validation error occurred, fix the errors and try again.",
				innerException)
		{ }
	}
}
