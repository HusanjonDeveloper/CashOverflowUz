//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class NotFoundLocationException : Xeption
	{
		public NotFoundLocationException(Guid locationId)
			: base(message: $"Couldn't find location with id: {locationId}.")
		{ }
	}
}
