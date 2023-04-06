using CashOverflowUz.Models.Locations;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace CashOverflowUz.Brokers.Storages
{
    public  partial interface IStorageBroker
    {
        ValueTask<Location> InsertLocationAysnc(Location location);
		IQueryable<Location> SelectAllLocations();
		ValueTask<Location> SelectLocationByIdAsync(Guid Id);
		ValueTask<Location> UpdateLocationAsync(Location location);
		ValueTask<Location> DeleteLocationAsync(Location location);

	}
}
