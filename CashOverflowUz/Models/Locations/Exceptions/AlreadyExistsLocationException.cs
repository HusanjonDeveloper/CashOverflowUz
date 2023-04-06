//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class AlreadyExistsLocationException : Xeption
	{
		public AlreadyExistsLocationException(Exception innerException)
			: base(message: "Location already exists.", innerException)
		{ }
	}
}
