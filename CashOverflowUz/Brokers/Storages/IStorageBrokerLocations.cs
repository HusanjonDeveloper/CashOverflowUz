using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Brokers.Storages
{
    public partial interface IStorageBrokerLocations
    {
        ValueTask<Location> InsertLocationAysnc(Location location);
    }
}
