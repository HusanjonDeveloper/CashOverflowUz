//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using Xeptions;

namespace CashOverflowUz.Models.Locations.Exceptions
{
    public class LocationValidationException : Xeption
    {
        public LocationValidationException(Xeption innerException)
        : base(message: "Location Valitadion error , fix the errors and try agin.",
             innerException)
        { }
    }
}
