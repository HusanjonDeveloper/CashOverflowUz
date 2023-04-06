//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class FailedReviewStorageException : Xeption
	{
		public FailedReviewStorageException(Exception innerException)
			: base(message: "Failed review storage error occurred, contact support.", innerException)
		{ }
	}
}
