using System.Threading.Tasks;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Services.Foundetions.Locations
{
    public class LocationService : ILocationService
    {
        public IStorageBroker StorageBroker;

        public LocationService(IStorageBroker storageBroker) =>
            StorageBroker = storageBroker;

        public ValueTask<Location> AddLocationAsyncs(Location location) =>
            throw new System.NotImplementedException();

    }
}
