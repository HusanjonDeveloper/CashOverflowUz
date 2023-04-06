using System.Linq;
using System;
using System.Threading.Tasks;
using CashOverflowUz.Models.job;
using CashOverflowUz.Models.Locations;

namespace CashOverflowUz.Brokers.Storages
{
	public partial interface IStorageBroker
	{
		ValueTask<Job> InsertJobAsync(Job job);
		IQueryable<Job> SelectAllJobs();
		ValueTask<Job> SelectJobByIdAsync(Guid jobId);
		ValueTask<Job> UpdateJobAsync(Job job);
		ValueTask<Job> DeleteJobAsync(Job job);
	}
}
