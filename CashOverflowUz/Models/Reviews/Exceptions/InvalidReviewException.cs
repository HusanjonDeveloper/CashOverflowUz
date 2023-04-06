//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class InvalidReviewException : Xeption
	{
		public InvalidReviewException()
			  : base(message: "Review is invalid.")
		{ }
	}
}
