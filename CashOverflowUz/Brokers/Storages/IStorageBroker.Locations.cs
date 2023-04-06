using System;
using System.Linq;
using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Brokers.Storages
{
	public partial interface IStorageBroker
	{
		ValueTask<Location> InsertLocationAysnc(Location location);
		IQueryable<Location> SelectAllLocations();
		ValueTask<Location> SelectLocationByIdAsync(Guid Id);
		ValueTask<Location> UpdateLocationAsync(Location location);
		ValueTask<Location> DeleteLocationAsync(Location location);

	}
}
