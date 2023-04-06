//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class FailedReviewServiceException : Xeption
	{
		public FailedReviewServiceException(Exception innerException)
			: base(message: "Failed review service error occured, please contact support", innerException)
		{ }
	}
}
