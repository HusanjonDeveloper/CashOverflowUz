//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class LocationDependencyValidationException : Xeption
	{
		public LocationDependencyValidationException(Xeption innerException)
			: base(message: "Location dependency validation error occurred, fix the errors and try again.", innerException)
		{ }
	}
}
