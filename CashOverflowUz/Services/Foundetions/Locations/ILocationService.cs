using System;
using System.Linq;
using System.Threading.Tasks;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Services.Foundetions.Locations
{
	public interface ILocationService
	{
		ValueTask<Location> AddLocationAsyncs(Location location);
		IQueryable<Location> RetrieveAllLocations();
		ValueTask<Location> RetrieveLocationByIdAsync(Guid locationId);
		ValueTask<Location> ModifyLocationAsync(Location location);
		ValueTask<Location> RemoveLocationByIdAsync(Guid locationId);
	}
}
