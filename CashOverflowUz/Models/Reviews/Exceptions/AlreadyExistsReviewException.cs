//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class AlreadyExistsReviewException : Xeption
	{
		public AlreadyExistsReviewException(Exception innerException)
			: base(message: "Review already exists.", innerException)
		{ }
	}
}
