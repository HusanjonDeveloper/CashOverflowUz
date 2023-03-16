using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Location> InsertLocationAysnc(Location location);

    }
}
