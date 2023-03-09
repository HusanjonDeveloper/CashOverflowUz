using System.Threading.Tasks;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Services.Foundetions.Locations
{
    public class LocationService : ILocationService
    {
        public IStorageBroker StorageBroker;

        public LocationService(IStorageBroker storageBroker, 
            Brokers.Loggings.ILoggingBroker loggingBroker) =>
            StorageBroker = storageBroker;

        public async ValueTask<Location> AddLocationAsyncs(Location location) =>
            await this.StorageBroker.InsertLocationAysnc(location);

    }
}
