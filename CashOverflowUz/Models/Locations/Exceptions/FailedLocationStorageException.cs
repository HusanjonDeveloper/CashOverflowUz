//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class FailedLocationStorageException : Xeption
	{
		public FailedLocationStorageException(Exception innerException)
			: base(message: "Failed location storage exception occurred, contact support.", innerException)
		{ }
	}
}
