using System.Threading.Tasks;
using CashOverflowUz.Models.job;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Brokers.Storages
{
	public partial interface IStorageBroker
	{
		ValueTask<Job> InsertJobAsync(Job job);
		Task<ValueTask<Location>> InsertLocationAsync(Location location);
		Task SelectLocationByIdAsync(object id);
	}
}
