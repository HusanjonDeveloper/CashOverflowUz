//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class LockedLocationException : Xeption
	{
		public LockedLocationException(Exception innerException)
			: base(message: "Locked Location record error, contact support.", innerException)
		{ }
	}
}
