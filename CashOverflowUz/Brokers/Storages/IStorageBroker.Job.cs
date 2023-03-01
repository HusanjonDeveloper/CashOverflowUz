using System.Threading.Tasks;
using CashOverflowUz.Models.job;

namespace CashOverflowUz.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<Job> InsertJobAsync(Job job);
    }
}
