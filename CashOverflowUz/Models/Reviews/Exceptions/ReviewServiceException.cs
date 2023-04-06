//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------


using System;
using Xeptions;

namespace CashOverflowUz.Models.Reviews.Exceptions
{
	public class ReviewServiceException : Xeption
	{
		public ReviewServiceException(Exception innerException)
			: base(message: "Review service error occured, contact support", innerException)
		{ }
	}
}
