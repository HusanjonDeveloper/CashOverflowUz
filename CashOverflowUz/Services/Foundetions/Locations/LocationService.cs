using System.Threading.Tasks;
using CashOverflowUz.Brokers.Loggings;
using CashOverflowUz.Brokers.Storages;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Services.Foundetions.Locations
{
    public partial class LocationService : ILocationService
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
        public  ValueTask<Location> AddLocationAsyncs(Location location) =>
            TryCatch(async ()=>
            {
                ValidateLocationNotNull(location);
                return await this.StorageBroker.InsertLocationAysnc(location);
            });
    }
}
