//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
    public class InvalidLocationException:Xeption
    {
        public InvalidLocationException()
        :base(message:"Location is invalid.")
        {}

    }
}
