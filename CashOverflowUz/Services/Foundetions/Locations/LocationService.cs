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
        private readonly IDateTimeBroker dateTimebroker;

        public LocationService(
            IStorageBroker storageBroker,
            ILoggingBroker loggingBroker,
            IDateTimeBroker dateTimeBroker)
        {
            this.StorageBroker = storageBroker;
            this.loggingBroker = loggingBroker;
            this.dateTimebroker = dateTimeBroker;
        }
        public ValueTask<Location> AddLocationAsyncs(Location location) =>
            TryCatch(async () =>
            {
                ValidateLocationOnAdd(location);
                return await this.StorageBroker.InsertLocationAysnc(location);
            });
    }
}
