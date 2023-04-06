//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class LocationDependencyException : Xeption
	{
		public LocationDependencyException(Xeption innerException)
			: base(message: "Location dependency error occurred, contact support.", innerException)
		{ }
	}
}
