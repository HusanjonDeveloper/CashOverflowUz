//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class FailedLocationServiceException : Xeption
	{
		public FailedLocationServiceException(Exception innerException)
			: base(message: "Failed location service error occurred, please contact support.", innerException)
		{ }
	}
}
