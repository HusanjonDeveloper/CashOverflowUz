//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class ReviewDependencyException : Xeption
	{
		public ReviewDependencyException(Xeption innerException)
			: base(message: "Review dependency error occured, contact support", innerException)
		{ }
	}
}
