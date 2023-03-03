using CashOverflowUz.Models.Locations;
using System.Threading.Tasks;

namespace CashOverflowUz.Brokers.Storages
{
    public  partial interface IStorageBroker
    {
        ValueTask<Location> InsertLocationAysnc(Location location);

    }
}
