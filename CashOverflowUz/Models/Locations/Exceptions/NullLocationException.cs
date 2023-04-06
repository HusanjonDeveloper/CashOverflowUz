//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
	public class NullLocationException : Xeption
	{
		public NullLocationException()
			: base(message: "Location is null.")
		{ }
	}
}
