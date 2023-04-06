//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class ReviewValidationException : Xeption
	{
		public ReviewValidationException(Xeption innerException)
			 : base(message: "Review validation error occurred, fix the errors and try again.", innerException)
		{ }
	}
}
