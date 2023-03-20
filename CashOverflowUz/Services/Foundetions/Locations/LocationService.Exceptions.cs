//-------------------------------------------------
// Copyright (c) Coalition OF Good-Hearted Engineers 
// Developet by CashOverflowUz Team
//--------------------------------------------------

using System;
using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Models.Locations.Exceptions;
using Microsoft.Data.SqlClient;
using Xeptions;

namespace CashOverflowUz.Services.Foundetions.Locations
{
    public partial class LocationService
    {
        private delegate ValueTask<Location> ReturningLocationFunction();

        private async ValueTask<Location> TryCatch(ReturningLocationFunction returningLocationFunction)
        {
            try
            {
                return await returningLocationFunction();
            }
            catch (NullLocationException nullLocationException)
            {
                throw CreateAndLogValidationException(nullLocationException);
            }
            catch (InvalidLocationException invalidLocationException)
            {
                throw CreateAndLogValidationException(invalidLocationException);
            }
            catch(SqlException sqlException)
            {
                var failedLocationStorageException = new FailedLocationStorageException(sqlException);
                throw CreateAndLogCriticalDependencyException(failedLocationStorageException);
            }
        }
        private LocationValidationException CreateAndLogValidationException(Xeption xeption)
        {
            var locationValidationException =
                 new LocationValidationException(xeption);

            this.loggingBroker.LogError(locationValidationException);
            return locationValidationException;
        }
        private LocationDependencyException CreateAndLogCriticalDependencyException(Xeption xeption)
        {
           var locationDependencyException = new LocationDependencyException(xeption);
            this.loggingBroker.LogCritical(locationDependencyException);
            
            return locationDependencyException;
        }
    }
}
