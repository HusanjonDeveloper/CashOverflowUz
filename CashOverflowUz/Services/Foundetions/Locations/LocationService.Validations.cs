//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using CashOverflowUz.Models.Locations.Exceptions;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Services.Foundetions.Locations
{
    public partial class LocationService
    {

        private static void ValidateLocationNotNull(Location location)
        {
            if (location is null)
            {
                throw new NullLocationException();
            }
        }
    }
}
