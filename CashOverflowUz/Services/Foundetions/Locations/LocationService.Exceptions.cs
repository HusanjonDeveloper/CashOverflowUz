//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Models.Locations.Exceptions;
using Xeptions;

namespace CashOverflowUz.Services.Foundetions.Locations
{
    public partial class LocationService
    {
        private  delegate ValueTask<Location> ReturningLocationFunction();
        
        private async ValueTask<Location> TryCatch(ReturningLocationFunction returningLocationFunction)
        {
            try
            {
                return await returningLocationFunction();
            }
            catch (NullLocationException nullLocationException)
            {
              throw  CreateAndLogValidationException(nullLocationException);
            }
            catch(InvalidLocationException invalidLocationException)
            {
                throw CreateAndLogValidationException(invalidLocationException);
            }

        }

        private LocationValidationException CreateAndLogValidationException( Xeption xeption)
        {
            var locationValidationException =
                 new LocationValidationException(xeption);

            this.loggingBroker.LogError(locationValidationException);
            return locationValidationException;
        }
    }
}
