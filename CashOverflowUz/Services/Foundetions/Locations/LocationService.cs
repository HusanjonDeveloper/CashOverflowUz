using System.Threading.Tasks;
using CashOverflowUz.Brokers.Loggings;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.Locations;
using CashOverflowUz.Models.Locations.Exceptions;

namespace CashOverflowUz.Services.Foundetions.Locations
{
    public class LocationService : ILocationService
    {
        private readonly IStorageBroker StorageBroker;
        private readonly ILoggingBroker loggingBroker;

        public LocationService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker)
        {
            this.StorageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
        }

        public async ValueTask<Location> AddLocationAsyncs(Location location)
        {
            try
            {
            if (location is null)
            {
                throw new NullLocationException();
            }
            return await this.StorageBroker.InsertLocationAysnc(location);

            }
            catch (NullLocationException nullLocationException)
            {
                var locationValidationException =
                     new LocationValidationException(nullLocationException);

                this.loggingBroker.LogError(locationValidationException);
                
                throw locationValidationException;
            }
        }
    }
}
