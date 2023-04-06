//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class LocationServiceException : Xeption
	{
		public LocationServiceException(Xeption innerException)
			: base(message: "Location service error occurred, contact support.", innerException)
		{ }
	}
}
