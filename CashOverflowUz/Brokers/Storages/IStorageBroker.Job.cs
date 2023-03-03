using System.Threading.Tasks;
using CashOverflowUz.Models.job;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Job> InsertJobAsync(Job job);
    }
}
