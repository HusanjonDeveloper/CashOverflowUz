//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class NullReviewException : Xeption
	{
		public NullReviewException()
			: base(message: "Review is null.")
		{ }
	}
}
